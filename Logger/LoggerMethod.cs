using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class LoggerMethod : ILoggerMethod, IDisposable
    {
        private readonly string logFilePath;
        private readonly BlockingCollection<string> logQueue;
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly Task loggingTask;

        public LoggerMethod(string logFileName)
        {
            string localPath = AppDomain.CurrentDomain.BaseDirectory;
            this.logFilePath = Path.Combine(localPath, logFileName);
            this.logQueue = new BlockingCollection<string>(new ConcurrentQueue<string>());
            this.cancellationTokenSource = new CancellationTokenSource();
            this.loggingTask = Task.Factory.StartNew(() => ProcessLogQueue(), TaskCreationOptions.LongRunning);
        }

        public void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        public void LogError(string message)
        {
            Log("ERROR", message);
        }

        private void Log(string level, string message)
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
            logQueue.Add(logMessage);
        }

        private void ProcessLogQueue()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true, Encoding.UTF8, 4096))
                {
                    foreach (var logMessage in logQueue.GetConsumingEnumerable(cancellationTokenSource.Token))
                    {
                        writer.WriteLine(logMessage);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Expected exception when the queue is being processed after cancellation
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process log queue: {ex.Message}");
            }
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            logQueue.CompleteAdding();
            try
            {
                loggingTask.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => e is OperationCanceledException);
            }
            cancellationTokenSource.Dispose();
            logQueue.Dispose();
        }
    }
}

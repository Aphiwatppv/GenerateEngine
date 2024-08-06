using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessMySQL.Model
{
    public class MachineData
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public double Voltage { get; set; }
        public double Current { get; set; }
        public double Vibration { get; set; }
        public DateTime TimestampDatetime { get; set; }
    }

}

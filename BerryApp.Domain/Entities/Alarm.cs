using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerryApp.Domain.Entities
{
    public class Alarm
    {
        public string Message { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
    }
    public enum AlarmState
    {
        New,
        Active,
        Acknowledged,
        Cleared
    }
}

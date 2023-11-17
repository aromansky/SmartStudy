using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStudy
{
    public class Event
    {
        public string group_id { get; }
        public string group_settings_id { get; }

        // Время в формате "DD.MM.YYYY HH:MM"
        public DateTime Time { get; set; } 

        public Event(string group_id, string group_settings_id, string time)
        {
            this.group_id = group_id;
            this.group_settings_id = group_settings_id;
            Time = DateTime.Parse(time);
        }

        public override string ToString()
        {
            return $"group_id: {group_id}, group_settings_id: {group_settings_id}, time: {Time}";
        }   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStudy
{
    public class Event
    {

        public long event_id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required DateTime date_begin { get; set; }
        public required DateTime date_end { get; set; }
        public Event(long event_id, string Title, string? Description, DateTime date_begin, DateTime date_end)
        {
            this.event_id = event_id;
            this.Title = Title;
            this.Description = Description;
            this.date_begin = date_begin;
            this.date_end = date_end;
        }
        public override string ToString()
        {
            return $"event_id: {event_id}, Title: {Title}, Description: {Description}, date_begin: {date_begin}, date_end: {date_end}"; 
        }   
    }
}

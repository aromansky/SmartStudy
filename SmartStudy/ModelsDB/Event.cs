namespace SmartStudy.ModelsDB
{
    public class Event
    {

        public long event_id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime date_begin { get; set; }
        public DateTime date_end { get; set; }
        public Event(string Title, string Description, DateTime date_begin, DateTime date_end)
        {
            this.Title = Title;
            this.Description = Description;
            this.date_begin = date_begin;
            this.date_end = date_end;
        }
        public Event() { }
        public override string ToString()
        {
            return $"event_id: {event_id}, Title: {Title}, Description: {Description}, date_begin: {date_begin}, date_end: {date_end}";
        }
    }
}

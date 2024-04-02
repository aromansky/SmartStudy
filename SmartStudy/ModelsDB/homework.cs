namespace SmartStudy.ModelsDB
{
    public class homework
    {
        public long homework_id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long? author_id { get; set; }
        public DateTime date_begin { get; set; }
        public DateTime date_end { get; set; }

        public homework(string title, string description, long? author_id, DateTime date_begin, DateTime date_end)
        {
            Title = title;
            Description = description;
            this.author_id = author_id;
            this.date_begin = date_begin;
            this.date_end = date_end;
        }
    }
}

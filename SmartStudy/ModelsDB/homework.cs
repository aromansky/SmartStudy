namespace SmartStudy.ModelsDB
{
    public class homework
    {
        public long homework_id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public long? author_id { get; set; }

    }
}

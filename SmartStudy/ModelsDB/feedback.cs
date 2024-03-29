namespace SmartStudy.ModelsDB
{
    public class feedback
    {
        public long feedback_id { get; set; }
        public long? author_id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}

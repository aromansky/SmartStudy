namespace SmartStudy.ModelsDB
{
    public class feedback
    {
        public long feedback_id { get; set; }
        public long author_id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public feedback(long author_id, string title, string description)
        {
            this.author_id = author_id;
            Title = title;
            Description = description;
        }
    }


    public class ModFeedback : feedback
    {
        public string author { get; set; }

        public ModFeedback(feedback fb, string author_firstname, string author_lastname): base(fb.author_id,
                                                                                               fb.Title, fb.Description)
        {
            this.feedback_id = fb.feedback_id;
            this.author = $"{author_lastname} {author_firstname}";
        }
    }
}

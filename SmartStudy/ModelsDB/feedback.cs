namespace SmartStudy.ModelsDB
{
    public class feedback
    {
        public long feedback_id { get; set; }
        public long author_id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }


    public class ModFeedback : feedback
    {
        public string author { get; set; }

        public ModFeedback(feedback fb, string author_firstname, string author_lastname)
        {
            this.feedback_id = fb.feedback_id;
            this.author_id = fb.author_id;
            this.Title = fb.Title;
            this.Description = fb.Description;
            this.author = $"{author_lastname} {author_firstname}";
        }
    }
}

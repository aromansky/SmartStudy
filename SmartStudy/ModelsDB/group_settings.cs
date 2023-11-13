namespace SmartStudy.ModelsDB
{
    public class group_settings
    {
        public long group_settings_id { get; set; }
        public long Tutor_id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }

        group_settings(int tutor_id, string title, string? description)
        {
            this.Tutor_id = tutor_id;
            this.Title = title;
            this.Description = description;
        }
    }
}

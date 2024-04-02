namespace SmartStudy.ModelsDB
{
    public class group_settings
    {
        public long group_settings_id { get; set; }
        public long Tutor_id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public group_settings(long tutor_id, string title, string? description)
        {
            this.Tutor_id = tutor_id;
            this.Title = title;
            this.Description = description;
        }



        public string IconAdd
        {
            get
            {
#if WINDOWS
                return "group_add_black.png";
#else
                return "group_add_black.svg";
#endif
            }
        }

        public string IconRemove
        {
            get
            {
#if WINDOWS
                return "group_remove_black.png";
#else
                return "group_remove_black.svg";
#endif
            }
        }
    }
}

namespace SmartStudy.ModelsDB
{
    public class group_event
    {
        public long group_event_id { get; set; }
        public long event_id { get; set; }
        public long group_settings_id { get; set; }

        public group_event(long event_id, long group_settings_id)
        {
            this.event_id = event_id;
            this.group_settings_id = group_settings_id;
        }
    }
}

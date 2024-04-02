namespace SmartStudy.ModelsDB
{
    public class group_homework
    {
        public long group_homework_id { get; set; }
        public long homework_id { get; set; }
        public long group_settings_id { get; set; }

        public group_homework(long homework_id, long group_settings_id)
        {
            this.homework_id = homework_id;
            this.group_settings_id = group_settings_id;
        }
    }
}

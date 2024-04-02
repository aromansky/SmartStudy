namespace SmartStudy.ModelsDB
{
    public class user_homework
    {
        public long user_homework_id { get; set; }
        public long homework_id { get; set; }
        public long user_id { get; set; }

        public user_homework(long homework_id, long user_id)
        {
            this.homework_id = homework_id;
            this.user_id = user_id;
        }
    }
}

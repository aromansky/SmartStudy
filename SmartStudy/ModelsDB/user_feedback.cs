namespace SmartStudy.ModelsDB
{
    public class user_feedback
    {
        public long group_feedback_id { get; set; }
        public long feedback_id { get; set; }
        public long user_id { get; set; }

        public user_feedback(long feedback_id, long user_id)
        {
            this.feedback_id = feedback_id;
            this.user_id = user_id;
        }
    }
}

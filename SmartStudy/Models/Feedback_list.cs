using SmartStudy.ModelsDB;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace SmartStudy.Models
{
    class Feedback_list
    {
        public ObservableCollection<feedback> UserFeedback { get; set; } = new ObservableCollection<feedback>();

#if WINDOWS
        private string _icon = "delete.png";
#else
        private string _icon = "group_add_black.svg";
#endif

        public string Icon
        {
            get => _icon;
            set { _icon = value; }
        }


        public Feedback_list() => LoadUserFeedback();

        public async void LoadUserFeedback()
        {
            List<feedback> feedbacks;

            User thisUser = Serializer.DeserializeUser();

            if (thisUser.IsTutor())
                feedbacks = await Client.GetFeedbackForAuthor(thisUser.user_id);
            else
                feedbacks = await Client.GetFeedbackForUser(thisUser.user_id);

            UserFeedback.Clear();
            foreach (feedback fb in feedbacks.OrderBy(x => x.Title))
                UserFeedback.Add(fb);
        }
    }
}

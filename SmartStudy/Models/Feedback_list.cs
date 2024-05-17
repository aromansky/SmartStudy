using SmartStudy.ModelsDB;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace SmartStudy.Models
{
    class Feedback_list
    {
        public ObservableCollection<ModFeedback> UserFeedback { get; set; } = new ObservableCollection<ModFeedback>();

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

            List<ModFeedback> feedbacksMod = new List<ModFeedback>();

            
            foreach (feedback fb in feedbacks.OrderBy(x => x.author_id))
            {
                User author = await Client.GetUserForID(fb.author_id);
                feedbacksMod.Add(new ModFeedback(fb, author.FirstName, author.LastName));
            }

            UserFeedback.Clear();
            foreach (ModFeedback mfb in feedbacksMod)
                UserFeedback.Add(mfb);
        }
    }
}

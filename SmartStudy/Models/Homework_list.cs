using SmartStudy.ModelsDB;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SmartStudy.Models
{
    class Homework_list
    {
        public ObservableCollection<homework> Homeworks { get; set; } = new ObservableCollection<homework>();
        public Homework_list() => LoadHomework();

        public async void LoadHomework()
        {  
            List<homework> homeworks;

            User thisUser = Serializer.DeserializeUser();

            if(thisUser.IsTutor())
                homeworks = await Client.GetHomeworkForAuthor(thisUser.user_id);
            else
                homeworks = await Client.GetUserHomework(thisUser.user_id);

            Homeworks.Clear();
            foreach (homework user in homeworks.OrderBy(x => x.Title))
                Homeworks.Add(user);
        }
    }
}

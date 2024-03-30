using SmartStudy.ModelsDB;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SmartStudy.Models
{
    class Homework_list
    {
        public ObservableCollection<homework> Homeworks { get; set; } = new ObservableCollection<homework>();
        public ObservableCollection<homework> GroupHomeworks { get; set; } = new ObservableCollection<homework>();

        //  стянул с Group_node
        public ObservableCollection<group_settings> GroupsWithUser { get; set; } = new ObservableCollection<group_settings>();
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

        public async void LoadGroupHomework(long g_s_id)
        {
            List<homework> homeworks;

            User thisUser = Serializer.DeserializeUser();

            homeworks = await Client.GetGroupHomework(g_s_id);

            GroupHomeworks.Clear();
            foreach (homework user in homeworks.OrderBy(x => x.Title))
                GroupHomeworks.Add(user);
        }


        //  стянул с Group_node
        public async void Load_Groups_With_User(long user_id)
        {
            if (Serializer.DeserializeUser().IsTutor())
            {
                List<group_settings> g_s = await Client.GetGroupsWithTutor(user_id);
                GroupsWithUser.Clear();
                foreach (group_settings g in g_s.OrderBy(x => x.Title))
                    GroupsWithUser.Add(g);
            }
            else
            {
                List<group_settings> g_s = await Client.GetGroupListWithUser(user_id);
                GroupsWithUser.Clear();
                foreach (group_settings g in g_s.OrderBy(x => x.Title))
                    GroupsWithUser.Add(g);
            }
        }
    }
}

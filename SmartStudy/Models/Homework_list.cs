using SmartStudy.ModelsDB;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace SmartStudy.Models
{
    class Homework_list
    {
        public ObservableCollection<homework> Homeworks { get; set; } = new ObservableCollection<homework>();
        public ObservableCollection<homework> GroupHomeworks { get; set; } = new ObservableCollection<homework>();
        public ObservableCollection<group_settings> GroupsWithHomework { get; set; } = new ObservableCollection<group_settings>();
        public ObservableCollection<group_settings> GroupsWithoutHomework { get; set; } = new ObservableCollection<group_settings>();

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


        public Homework_list() => LoadHomework();

        public async void LoadHomework()
        {
            List<homework> homeworks;

            User thisUser = Serializer.DeserializeUser();

            if (thisUser.IsTutor())
                homeworks = await Client.GetHomeworkForAuthor(thisUser.user_id);
            else
                homeworks = await Client.GetUserHomework(thisUser.user_id);

            Homeworks.Clear();
            foreach (homework user in homeworks.OrderBy(x => x.Title))
                Homeworks.Add(user);
        }

        public async void LoadGroupHomework(long g_s_id)
        {
            User thisUser = Serializer.DeserializeUser();

            List<homework> homeworks = await Client.GetGroupHomework(g_s_id);

            GroupHomeworks.Clear();
            foreach (homework hw in homeworks.OrderBy(x => x.Title))
                GroupHomeworks.Add(hw);
        }

        public async void LoadGroupsWithHomework(long hw_id)
        {
            User thisUser = Serializer.DeserializeUser();

            List<group_settings> groups = await Client.GetGroupsWithHomework(hw_id);

            GroupsWithHomework.Clear();
            foreach (group_settings g_s in groups.OrderBy(x => x.Title))
                GroupsWithHomework.Add(g_s);
        }

        public async void LoadGroupsWithoutHomework(long hw_id)
        {
            User thisUser = Serializer.DeserializeUser();

            List<group_settings> groupsWithHw = await Client.GetGroupsWithHomework(hw_id);
            List<group_settings> groups = await Client.GetGroupsWithTutor(thisUser.user_id);

            GroupsWithoutHomework.Clear();
            foreach (group_settings g_s in groups.Where(x => !groupsWithHw.Select(x => x.group_settings_id).Contains(x.group_settings_id)).OrderBy(x => x.Title))
                GroupsWithoutHomework.Add(g_s);
        }
    }
}

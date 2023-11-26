using SmartStudy.ModelsDB;
using System.Collections.ObjectModel;

namespace SmartStudy.Models
{
    class User_list
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public ObservableCollection<User> UsersInGroup { get; set; } = new ObservableCollection<User>();

        public User_list() => Load_All_Users();

        public async void Load_All_Users()
        {
            List<User> users = await Client.GetUsersList();
            Users.Clear();
            foreach (User user in users)
                Users.Add(user);
        }

        public async void Load_Users_In_Group(long group_settings_id)
        {
            List<User> users = await Client.GetUsersFromGroup(group_settings_id);
            UsersInGroup.Clear();
            foreach (User user in users)
                UsersInGroup.Add(user);
        }
    }
}

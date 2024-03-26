using SmartStudy.ModelsDB;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SmartStudy.Models
{
        class User_list
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public ObservableCollection<User> UsersInGroup { get; set; } = new ObservableCollection<User>();
        public ObservableCollection<User> UsersOutsideGroup { get; set; } = new ObservableCollection<User>();
        public ObservableCollection<object> SelectedUsers { get; set; } = new ObservableCollection<object>();

        public User_list() => Load_All_Users();

        public async void Load_All_Users()
        {
            List<User> users = await Client.GetUsersList();
            Users.Clear();
            foreach (User user in users.OrderBy(x => x.LastName))
                Users.Add(user);
        }

        public async void Load_Users_In_Group(long group_settings_id)
        {
            List<User> users = await Client.GetUsersFromGroup(group_settings_id);
            UsersInGroup.Clear();
            foreach (User user in users.OrderBy(x => x.LastName))
                UsersInGroup.Add(user);
        }

        public async void Load_Users_Outside_Group(long group_settings_id)
        {
            
            List<User> users = await Client.GetUsersList();
            List<User> usersInGroup = await Client.GetUsersFromGroup(group_settings_id);
            UsersOutsideGroup.Clear();
            foreach (User user in users.Where(x => !usersInGroup.Select(x => x.user_id).Contains(x.user_id)).OrderBy(x => x.LastName))
                UsersOutsideGroup.Add(user);
        }
    }
}

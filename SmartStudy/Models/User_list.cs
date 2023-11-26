using SmartStudy.ModelsDB;
using System.Collections.ObjectModel;

namespace SmartStudy.Models
{
    class User_list
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public User_list() => Load_All_Users();

        public async void Load_All_Users()
        {
            List<User> users = await Client.GetUsersList();
            Users.Clear();
            foreach (User user in users)
                Users.Add(user);
        }
    }
}

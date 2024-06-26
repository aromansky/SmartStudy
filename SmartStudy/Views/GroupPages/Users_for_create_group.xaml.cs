using SmartStudy.Models;
using SmartStudy.ModelsDB;
using System.Globalization;

namespace SmartStudy.Views.GroupPages;

public partial class Users_for_create_group : ContentPage
{
    List<long> users;
    public Users_for_create_group(ref List<long> users)
	{
		InitializeComponent();
        BindingContext = new User_list();
        this.users = users;
    }

    protected override async void OnAppearing()
    {
        (BindingContext as User_list).Load_Users_For_Group(users);
    }


    private void UpdateUsers(User user)
    {
        (BindingContext as User_list).UsersForGroup.Remove(user);
    }

    private async void add_user(object sender, EventArgs e)
    {
        User user = (sender as ImageButton).BindingContext as User;
        users.Add(user.user_id);
        UpdateUsers(user);
    }
}

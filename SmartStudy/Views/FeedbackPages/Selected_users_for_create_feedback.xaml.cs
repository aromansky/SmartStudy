using SmartStudy.Models;
using SmartStudy.ModelsDB;
using System.Globalization;

namespace SmartStudy.Views.FeedbackPages;

public partial class Selected_users_for_create_feedback : ContentPage
{
    List<long> users;
    public Selected_users_for_create_feedback(ref List<long> users)
	{
		InitializeComponent();
        BindingContext = new User_list();
        this.users = users;
    }

    protected override async void OnAppearing()
    {
        (BindingContext as User_list).Load_Selected_Users(users);
    }


    private void UpdateUsers(User user)
    {
        (BindingContext as User_list).SelectedUsers.Remove(user);
    }

    private async void delete_user(object sender, EventArgs e)
    {
        User user = (sender as ImageButton).BindingContext as User;
        //await Client.AddUsersToGroup(group_id, user.user_id);
        users.Remove(user.user_id);
        UpdateUsers(user);
    }
}

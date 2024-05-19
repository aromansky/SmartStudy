using SmartStudy.ModelsDB;
using SmartStudy.Models;

namespace SmartStudy.Views.FeedbackPages;
public partial class Create_feedback : ContentPage
{
    List<long> users;
    List<long> groups;
    public Create_feedback()
	{
#if WINDOWS
        ToolbarItem add_users = new ToolbarItem { IconImageSource = ImageSource.FromFile("user_add_white.png") };
        ToolbarItem add_groups = new ToolbarItem { IconImageSource = ImageSource.FromFile("group_add_white.png") };
        ToolbarItem users_bar = new ToolbarItem { IconImageSource = ImageSource.FromFile("users_white.png") };
#else
        ToolbarItem add_users = new ToolbarItem { IconImageSource = ImageSource.FromFile("user_add_white.svg") };
        ToolbarItem add_groups = new ToolbarItem { IconImageSource = ImageSource.FromFile("group_add_white.png") };
        ToolbarItem users_bar = new ToolbarItem { IconImageSource = ImageSource.FromFile("users_white.svg") };
#endif
        add_users.Clicked += AddUsers;
        add_groups.Clicked += AddGroup;
        users_bar.Clicked += UsersWithFeedback;

        this.ToolbarItems.Add(users_bar);
        this.ToolbarItems.Add(add_groups);
        this.ToolbarItems.Add(add_users);

        users = new List<long>();
        groups = new List<long>();

        InitializeComponent();
    }

    private async void Savebutton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Title.Text))
            await DisplayAlert("Ошибка", "Введите заголовок", "Ок");
        else
        {
            AddUsersFromGroups();
            await Client.CreateFeedback(new feedback(Serializer.DeserializeUser().user_id, Title.Text, Description.Text));
            long id = await Client.GetLastCreatedFeedbackId();
            await Client.SendFeedbackToUsers(id, users.ToArray());
            await Shell.Current.GoToAsync("///feedback");
        }
    }
    private async void AddUsers(object sender, EventArgs e)
    {
        await AddUsersFromGroups();
        await Navigation.PushAsync(new Users_for_create_feedback(ref users));
    }

    private async void UsersWithFeedback(object sender, EventArgs e)
    {
        await AddUsersFromGroups();
        await Navigation.PushAsync(new Selected_users_for_create_feedback(ref users));
    }

    private async void AddGroup(object sender, EventArgs e)
    {
        groups = groups.Distinct().ToList();
        await Navigation.PushAsync(new Groups_for_create_feedback(ref groups));
    }

    private async Task<bool> AddUsersFromGroups()
    {
        foreach (long g_s_id in groups)
        {
            List<User> u = await Client.GetUsersFromGroup(g_s_id);
            users = users.Concat(u.Select(x => x.user_id)).Distinct().ToList();
        }
        return true;
    }
}
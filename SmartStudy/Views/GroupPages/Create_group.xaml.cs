using SmartStudy.ModelsDB;
using SmartStudy.Models;

namespace SmartStudy.Views.GroupPages;

public partial class Create_group : ContentPage
{
    List<long> users;
    public Create_group()
    {
#if WINDOWS
        ToolbarItem add_users = new ToolbarItem { IconImageSource = ImageSource.FromFile("user_add_white.png") };
        ToolbarItem users_bar = new ToolbarItem { IconImageSource = ImageSource.FromFile("groups_white.png") };
#else
        ToolbarItem add_users = new ToolbarItem { IconImageSource = ImageSource.FromFile("user_add_white.svg") };
        ToolbarItem users_bar = new ToolbarItem { IconImageSource = ImageSource.FromFile("groups1.svg") };
#endif
        add_users.Clicked += AddUsers;
        users_bar.Clicked += UsersInGroup;

        this.ToolbarItems.Add(users_bar);
        this.ToolbarItems.Add(add_users);

        users = new List<long>();

        InitializeComponent();
    }

    private async void Savebutton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Title.Text))
            await DisplayAlert("Ошибка", "Введите название группы", "Ок");
        else
        {
            group_settings g_s = new group_settings(Serializer.DeserializeUser().user_id, Title.Text, Description.Text);
            await Client.CreateGroupSettings(g_s);
            long g_s_id = await Client.GetLastCreatedGroupId();
            await Client.AddUsersToGroup(g_s_id, users.ToArray());
            await Shell.Current.GoToAsync("///groups");
        }
    }

    private async void AddUsers(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Users_for_create_group(ref users));
    }

    private async void UsersInGroup(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Selected_users_for_create_group(ref users));
    }
}
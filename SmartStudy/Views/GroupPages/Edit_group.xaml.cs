using SmartStudy.ModelsDB;
using SmartStudy.Models;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace SmartStudy.Views.GroupPages;

[QueryProperty(nameof(Group_get_Id), "group_settings_id")]
public partial class Edit_group : ContentPage
{
    long group_settings_id;
    List<User> seleted_users = new List<User>();
    User user = Serializer.DeserializeUser();
    public long Group_get_Id
    {
        set { LoadGroup_id(value); }
    }
    private async void LoadGroup_id(long text_obj)
    {
        group_settings_id = text_obj;
        group_settings group_settings = await Client.GetGroupWithId(text_obj);
        Title.Text = group_settings.Title;
        Description.Text = group_settings.Description;
    }

    public Edit_group()
    {
#if WINDOWS
ToolbarItem users = new ToolbarItem { IconImageSource = ImageSource.FromFile("groups_white.png") };
users.Clicked += UsersInGroup;
this.ToolbarItems.Add(users);

if(user.IsTutor())
        {
            ToolbarItem add_users = new ToolbarItem { IconImageSource = ImageSource.FromFile("user_add_white.png") };

            add_users.Clicked += AddUsersToGroup;

            if (this.ToolbarItems.Count == 1)
                this.ToolbarItems.Add(add_users);
        } 
#else
        ToolbarItem users = new ToolbarItem { IconImageSource = ImageSource.FromFile("groups1.svg") };
        users.Clicked += UsersInGroup;
        this.ToolbarItems.Add(users);

        if (user.IsTutor())
        {
            ToolbarItem add_users = new ToolbarItem { IconImageSource = ImageSource.FromFile("user_add_white.svg") };

            add_users.Clicked += AddUsersToGroup;

            if (this.ToolbarItems.Count == 1)
                this.ToolbarItems.Add(add_users);
        }
#endif

        InitializeComponent();
        BindingContext = new User_list();

    }

    private void EditButton_Clicked(object sender, EventArgs e)
    {
        Title.IsReadOnly = false;
        Description.IsReadOnly = false;
        SaveButton.IsEnabled = true;
        SaveButton.IsVisible = true;
    }

    protected override void OnAppearing()
    {
        if (!Serializer.DeserializeUser().IsTutor())
        {
            EditButton.IsEnabled = false;
            EditButton.IsVisible = false;

            DeleteButton.IsEnabled = false;
            DeleteButton.IsVisible = false;
        }
    }

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        User user = args.SelectedItem as User;
        seleted_users.Add(user);
    }


    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Подтвердить действие", $"Вы хотите удалить группу?", "Да", "Нет");
        if (result)
        {
            Client.DeleteGroupSettings(group_settings_id);
            await Shell.Current.GoToAsync("///groups");
        }
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        group_settings group_Settings = new group_settings(Serializer.DeserializeUser().user_id, Title.Text, Description.Text);
        group_Settings.group_settings_id = group_settings_id;
        Client.EditGroupFromId(group_settings_id, group_Settings);
        await Shell.Current.GoToAsync("///groups");
    }

    private async void AddUsersToGroup(object sender, EventArgs e)
    {
        group_settings group_Settings = new group_settings(Serializer.DeserializeUser().user_id, Title.Text, Description.Text);
        group_Settings.group_settings_id = group_settings_id;
        Client.EditGroupFromId(group_settings_id, group_Settings);
        await Shell.Current.GoToAsync($"users_outside_group?group_id={group_settings_id}");
    }

    private async void UsersInGroup(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"users_in_group?group_id={group_settings_id}");
    }
}

using SmartStudy.ModelsDB;

namespace SmartStudy.Views.GroupPages;

[QueryProperty(nameof(Group_get_id), "group_settings_id")]
public partial class Users_list : ContentPage
{
    long group_settings_id;

    public long Group_get_id
    {
        set { LoadGroup_id(value); }
    }

    private void LoadGroup_id(long text_obj)
    {
        group_settings_id = text_obj;
        ((Models.User_list)BindingContext).Load_Users_Outside_Group(group_settings_id);
    }
    public Users_list()
	{
		InitializeComponent();
        BindingContext = new Models.User_list();
    }

    private async void ok_Clicked(object sender, EventArgs e)
    {
        if (((Models.User_list)BindingContext).SelectedUsers.Count() == 0)
        {
            DisplayAlert("Ошибка", "Выберите не менее 1 пользователя", "Ok");
            return;
        }
        User[] users = ((Models.User_list)BindingContext).SelectedUsers.Select(x => x as User).ToArray();
        Client.AddUsersToGroup(group_settings_id, users);
        await Shell.Current.GoToAsync("///groups");
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
    }
}
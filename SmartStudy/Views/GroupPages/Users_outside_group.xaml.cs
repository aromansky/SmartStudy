using SmartStudy.Models;
using SmartStudy.ModelsDB;
using System.Globalization;

namespace SmartStudy.Views.GroupPages;

[QueryProperty(nameof(Group_get_Id), "group_id")]
public partial class Users_outside_group : ContentPage
{
    private long group_id;
    public long Group_get_Id
    {
        set { group_id = value; }
    }
    public Users_outside_group()
	{

		InitializeComponent();
        BindingContext = new User_list();
        
    }

    protected override void OnAppearing()
    {
        UpdateUsers(group_id);
    }


    private void UpdateUsers(long group_settings_id)
    {
        (BindingContext as User_list).Load_Users_Outside_Group(group_settings_id);
    }

    private async void add_user(object sender, EventArgs e)
    {
        await Client.AddUsersToGroup(group_id, ((sender as ImageButton).BindingContext as User).user_id);
        UpdateUsers(group_id);
    }
}

public class FullNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is User user)
        {
            return $"{user.LastName} {user.FirstName}";
        }

        return string.Empty;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
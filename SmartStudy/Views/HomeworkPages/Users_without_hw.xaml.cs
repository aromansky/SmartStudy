using SmartStudy.Models;
using SmartStudy.ModelsDB;
using System.Globalization;

namespace SmartStudy.Views.HomeworkPages;

[QueryProperty(nameof(Hw_get_Id), "hw_id")]
public partial class Users_without_hw : ContentPage
{
    private long hw_id;
    public long Hw_get_Id
    {
        set { hw_id = value; }
    }
    public Users_without_hw()
	{

		InitializeComponent();
        BindingContext = new User_list();
        
    }

    protected override void OnAppearing()
    {
        UpdateUsers(hw_id);
    }


    private void UpdateUsers(long homework_id)
    {
        (BindingContext as User_list).Load_Users_Without_Homework(homework_id);
    }

    private async void add_user(object sender, EventArgs e)
    {
        await Client.CreateUserHomework(hw_id, ((sender as ImageButton).BindingContext as User).user_id);
        UpdateUsers(hw_id);
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
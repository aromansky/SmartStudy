using SmartStudy.ModelsDB;
using SmartStudy.Models;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace SmartStudy.Views.Student;

[QueryProperty(nameof(Group_get_Id), "group_settings_id")]
public partial class Edit_feedback : ContentPage
{
    long group_settings_id;
    List<User> seleted_users = new List<User>();
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

    public Edit_feedback()
	{
		InitializeComponent();
        BindingContext = new User_list();
        
    }

    protected override void OnAppearing()
    {
        ((User_list)BindingContext).Load_Users_In_Group(group_settings_id);
    }

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        User user = args.SelectedItem as User;
        seleted_users.Add(user);
    }

    /*
    void OnCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        CollectionView collectionView = new CollectionView();
        collectionView.Scrolled += OnCollectionViewScrolled;
    }
    */

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
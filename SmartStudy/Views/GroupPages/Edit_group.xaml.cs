using SmartStudy.ModelsDB;
using SmartStudy.Models;

namespace SmartStudy.Views.GroupPages;

[QueryProperty(nameof(Group_get_Id), "group_settings_id")]
public partial class Edit_group : ContentPage
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

    public Edit_group()
	{
		InitializeComponent();
        BindingContext = new Models.User_list();
        
    }

    private void EditButton_Clicked(object sender, EventArgs e)
    {
        Title.IsEnabled = true;
        Description.IsEnabled = true;
        AddUsers.IsEnabled = true;
        AddUsers.IsVisible = true;
    }

    protected override void OnAppearing()
    {
        ((User_list)BindingContext).Load_Users_In_Group(group_settings_id);
        if(!Serializer.DeserializeUser().IsTutor())
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

    /*
    void OnCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        CollectionView collectionView = new CollectionView();
        collectionView.Scrolled += OnCollectionViewScrolled;
    }
    */

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
    }
}
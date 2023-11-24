using SmartStudy.ModelsDB;
using SmartStudy.Models;
using Microsoft.Maui.Controls;

namespace SmartStudy.Views.Teacher;

public partial class Create_group : ContentPage
{
    List<User> seleted_users = new List<User>();

	public Create_group()
	{
		InitializeComponent();
        BindingContext = new Models.User_list();
        
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        //TODO
        if (string.IsNullOrEmpty(Title.Text))
            await DisplayAlert("Ошибка", "Введите название группы", "Ок");
        else
        {
            group_settings g_s = new group_settings(Serializer.DeserializeUser().user_id, Title.Text, Description.Text);
            Client.CreateGroupSettings(g_s);

            //TODO теперь поработать с добавлением/удалением учеников
            await Shell.Current.GoToAsync("///groups");
        }
        
    }

    protected override void OnAppearing()
    {
        ((Models.User_list)BindingContext).Load_All_Users();
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

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
    }
}
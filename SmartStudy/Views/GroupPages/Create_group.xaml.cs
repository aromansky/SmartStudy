using SmartStudy.ModelsDB;
using SmartStudy.Models;

namespace SmartStudy.Views.GroupPages;

public partial class Create_group : ContentPage
{
    public Create_group()
	{
		InitializeComponent();
    }

    private async void Savebutton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Title.Text))
            await DisplayAlert("Ошибка", "Введите название группы", "Ок");
        else
        {
            group_settings g_s = new group_settings(Serializer.DeserializeUser().user_id, Title.Text, Description.Text);
            Client.CreateGroupSettings(g_s);
            await Shell.Current.GoToAsync("///groups");
        }
    }
}
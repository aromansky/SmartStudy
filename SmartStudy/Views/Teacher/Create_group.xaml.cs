using SmartStudy.ModelsDB;
using SmartStudy.Models;

namespace SmartStudy.Views.Teacher;

public partial class Create_group : ContentPage
{
	public Create_group()
	{
		InitializeComponent();
	}

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        //TODO
        if (string.IsNullOrEmpty(Title.Text))
            DisplayAlert("Ошибка", "Введите название группы", "Ок");
        else
        {
            group_settings g_s = new group_settings(Serializer.DeserializeUser().user_id, Title.Text, Description.Text);
            Client.CreateGroupSettings(g_s);

            //TODO теперь поработать с добавлением/удалением учеников
            await Shell.Current.GoToAsync("///groups");
        }
        
    }
    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
    }
}
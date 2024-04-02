using SmartStudy.ModelsDB;
using SmartStudy.Models;

namespace SmartStudy.Views.Teacher;
public partial class Create_feedback : ContentPage
{
    public Create_feedback()
	{
		InitializeComponent();
    }

    private async void Savebutton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Title.Text))
            await DisplayAlert("Ошибка", "Введите название фидбэка", "Ок");
        else
        {
            group_settings g_s = new group_settings(Serializer.DeserializeUser().user_id, Title.Text, Description.Text);
            Client.CreateGroupSettings(g_s); // вот тут заменить на feedbackSettings?
            await Shell.Current.GoToAsync("///feedback");
        }
    }

    private void AddUsersToFeedback(object sender, EventArgs e)
    {
        DisplayAlert("Заглушка", "На данный момент эта кнопка не работает", "Ок");
        //bool res = await DisplayAlert("Создание группы", "Сохранить указанные настройки и перейти к добавлению пользователей?", "Да", "Нет");
        //if(res)
        //{
        //    if (string.IsNullOrEmpty(Title.Text))
        //        await DisplayAlert("Ошибка", "Введите название группы", "Ок");
        //    else
        //    {
        //        long user_id = Serializer.DeserializeUser().user_id;
        //        group_settings g_s = new group_settings(user_id, Title.Text, Description.Text);
        //        Client.CreateGroupSettings(g_s);
        //        // надо будет подумать над переходом к странице добавления пользователей
               
        //    }
        //}
    }
}
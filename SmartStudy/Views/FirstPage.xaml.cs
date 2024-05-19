using System.Diagnostics;

namespace SmartStudy;

public partial class FirstPage : ContentPage
{
    public FirstPage()
    {
        InitializeComponent();
    }

    private async void ClickRegister(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrationPage());
    }

    private async void ClickLogInStudent(object sender, EventArgs e)
    {
        bool res = await Client.Login(EMail.Text, Password.Text, "Student");
        if (res)
        {
            Application.Current.MainPage = new Views.Student.AppShell_Student();
            await Shell.Current.GoToAsync("///calendar");
        }
        else
            DisplayAlert("Ошибка", "Неверный логин или пароль", "ОК");
    }

    private async void ClickLogInTeacher(object sender, EventArgs e)
    {
        bool res = await Client.Login(EMail.Text, Password.Text, "Tutor");
        if (res)
        {
            Application.Current.MainPage = new Views.Teacher.AppShell_Teacher();
            await Shell.Current.GoToAsync("///calendar");
        }
        else
            DisplayAlert("Ошибка", "Неверный логин или пароль", "ОК");
    }

    private void ClickPasswordVisibility(object sender, EventArgs e)
    {
        Password.IsPassword = !Password.IsPassword;
        var button = sender as ImageButton;
        if (button != null)
        {
            if (button.Source.ToString().Contains("opened.png"))
                button.Source = "closed.png"; // Изменение на изображение "closed.png"
            else
                button.Source = "opened.png"; // Изменение обратно на изображение "opened.png"
        }
        else
            Debug.WriteLine("Error: sender is not ImageButton");
    }
}
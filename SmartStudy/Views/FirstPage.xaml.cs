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
        bool res = await Client.Login(EMail.Text, Password.Text);
        if (res)
            await Navigation.PushModalAsync(new Views.Student.MainPage_Student());
        else
            DisplayAlert("Ошибка", "Неверный логин или пароль", "ОК");
    }

    private void ClickForgotPassword(object sender, EventArgs e)
    {

    }

    private void ClickPasswordVisibility(object sender, EventArgs e)
    {
        Password.IsPassword = !Password.IsPassword;
    }
}
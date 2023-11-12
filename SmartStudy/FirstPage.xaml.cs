using System.Diagnostics;

namespace SmartStudy;

public partial class FirstPage : ContentPage
{
	public FirstPage()
	{
		InitializeComponent();
	}
  
    private void ClickRegister(object sender, EventArgs e)
    {

    }

    private async void ClickLogIn(object sender, EventArgs e)
    {
        bool res = await Client.Login(EMail.Text, Password.Text);
        if (res)
            await Navigation.PushAsync(new MainPage());
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
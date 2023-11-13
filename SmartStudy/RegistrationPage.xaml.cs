using Microsoft.Maui.ApplicationModel.Communication;
using System.Text.RegularExpressions;

namespace SmartStudy;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
    }

    private async void CreateAccount(object sender, EventArgs e)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        Match isMatch = Regex.Match(EMail.Text is null ? "" : EMail.Text, pattern, RegexOptions.IgnoreCase);
        if (!isMatch.Success)
        {
            DisplayAlert("Неверный формат почты", "Пожалуйста, проверьте правильность ввода почты", "ОК");
            return;
        }
        string recommendations = Client.IsGoodPassword(Password.Text);
        if (recommendations != "")
            DisplayAlert("Пароль не подходит", recommendations, "ОК");
        else
        {
            if (Password.Text == RepeatPassword.Text)
            {
                Client.Register(FirstName.Text, LastName.Text, EMail.Text, Password.Text);
                await Navigation.PushAsync(new MainPage());
            }
            else
                DisplayAlert("Пароли не совпадают", "Пожалуйста, проверьте правильность ввода пароля", "ОК");
        }
    }

    private void ClickPasswordVisibility(object sender, EventArgs e)
    {
        Password.IsPassword = !Password.IsPassword;
    }
}
using SmartStudy.ModelsDB;
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
            DisplayAlert("�������� ������ �����", "����������, ��������� ������������ ����� �����", "��");
            return;
        }
        string recommendations = Client.IsGoodPassword(Password.Text);
        if (recommendations != "")
            DisplayAlert("������ �� ��������", recommendations, "��");
        else
        {
            if (Password.Text == RepeatPassword.Text)
            {
                User user = new User(FirstName.Text, LastName.Text, EMail.Text, Password.Text);
                Client.Register(user);
                await Navigation.PushAsync(new FirstPage());
            }
            else
                DisplayAlert("������ �� ���������", "����������, ��������� ������������ ����� ������", "��");
        }
    }

    private void ClickPasswordVisibility(object sender, EventArgs e)
    {
        Password.IsPassword = !Password.IsPassword;
    }
}
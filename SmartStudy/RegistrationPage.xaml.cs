namespace SmartStudy;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
    }

    private void CreateAccount(object sender, EventArgs e)
    {
        string recommendations = Client.IsGoodPassword(Password.Text);
        if (recommendations != "")
            DisplayAlert("������ �� ��������", recommendations, "��");
        else
        {
            if (Password.Text == RepeatPassword.Text)
                Client.Register(FirstName.Text, LastName.Text, EMail.Text, Password.Text);
            else
                DisplayAlert("������ �� ���������", "����������, ��������� ������������ ����� ������", "��");
        }
    }

    private void ClickPasswordVisibility(object sender, EventArgs e)
    {
        Password.IsPassword = !Password.IsPassword;
    }
}
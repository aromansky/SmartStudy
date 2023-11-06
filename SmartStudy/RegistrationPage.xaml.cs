namespace SmartStudy;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
	}

    private void CreateAccount(object sender, EventArgs e)
    {
        if(Password.Text == RepeatPassword.Text)
        {
            Client.Register(FirstName.Text, LastName.Text, EMail.Text, Password.Text);
        }
    }

    private void ClickPasswordVisibility(object sender, EventArgs e)
    {
        Password.IsPassword = !Password.IsPassword;
    }
}
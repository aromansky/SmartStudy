namespace SmartStudy;

public partial class FirstPage : ContentPage
{
	public FirstPage()
	{
		InitializeComponent();
	}
  
    private void ClickRegister(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegistrationPage());
    }

    private void ClickLogIn(object sender, EventArgs e)
    {

    }

    private void ClickForgotPassword(object sender, EventArgs e)
    {

    }

    private void ClickPasswordVisibility(object sender, EventArgs e)
    {
        Password.IsPassword = !Password.IsPassword;
    }
}
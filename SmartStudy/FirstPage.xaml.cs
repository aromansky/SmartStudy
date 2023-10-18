namespace SmartStudy;

public partial class FirstPage : ContentPage
{
	public FirstPage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.Window.MinimumHeight = 600;
        this.Window.MinimumWidth = 600;
    }
    private void ClickRegister(object sender, EventArgs e)
    {

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
namespace SmartStudy;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.Window.MinimumHeight = 600;
        this.Window.MinimumWidth = 600;
    }
}

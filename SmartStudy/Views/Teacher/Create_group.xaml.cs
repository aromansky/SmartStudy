namespace SmartStudy.Views.Teacher;

public partial class Create_group : ContentPage
{
	public Create_group()
	{
		InitializeComponent();
	}

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        //TODO
        await Shell.Current.GoToAsync("///groups");
    }
    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
    }
}
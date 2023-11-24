using SmartStudy.Models;

namespace SmartStudy.Views.CalendarPages;

public partial class Group_list : ContentPage
{
    public Group_list()
    {
        InitializeComponent();
        BindingContext = new Models.Group_note();
    }
    public async void clicked_to_main_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///main_page");
    }
    public async void clicked_to_groups(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
    }
    public async void clicked_to_feedback(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///feedback");
    }
    public async void clicked_to_homework(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///homework");
    }
    protected override void OnAppearing()
    {
        ((Models.Group_note)BindingContext).Load_All_Groups();
    }
    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///calendar");
    }
}
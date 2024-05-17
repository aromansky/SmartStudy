using SmartStudy.Models;

namespace SmartStudy.Views.Student;

public partial class Feedback : ContentPage
{
	public Feedback()
	{
		InitializeComponent();
        BindingContext = new Feedback_list();
#if WINDOWS
#else
        row_button.Height = 0;
#endif
    }
    public async void update_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///main_page");
        await Shell.Current.GoToAsync("///feedback");
    }
    public async void clicked_to_main_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///main_page");
    }
    public async void clicked_to_calendar(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///calendar");
    }
    public async void clicked_to_homework(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///homework");
    }
    public async void clicked_to_groups(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
    }



    protected override void OnAppearing()
    {
        (BindingContext as Feedback_list).LoadUserFeedback();
    }


    private async void feedback_clicked(object sender, SelectionChangedEventArgs e)
    {
        //if (e.CurrentSelection.Count != 0)
        //{
        //    await Shell.Current.GoToAsync($"edit_group?group_settings_id={((ModelsDB.group_settings)e.CurrentSelection[0]).group_settings_id}");
        //    all_groups.SelectedItem = null;
        //}

    }
}
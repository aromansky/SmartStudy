using SmartStudy.Models;

namespace SmartStudy.Views.Student;

public partial class Feedback : ContentPage
{
	public Feedback()
	{
		InitializeComponent();
        grid.SetColumnSpan(main_view, 2);
        Label lab = new Label();
        lab.Text = "װטהבוך";
        main_view.Add(lab);
#if WINDOWS
#else
        row_button.Height = 0;
#endif
        BindingContext = new Group_note();
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
        //((Group_note)BindingContext).Load_Groups_With_User();
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
namespace SmartStudy.Views.Student;

public partial class Homework : ContentPage
{
	public Homework()
	{
		InitializeComponent();
        grid.SetColumnSpan(main_view, 2);
        Label lab = new Label();
        lab.Text = "Задания от учителя";
        main_view.Add(lab);
#if WINDOWS
#else
        row_button.Height = 0;
#endif
    }
    public async void clicked_to_main_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///main_page");
    }
    public async void clicked_to_calendar(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///calendar");
    }
    public async void clicked_to_feedback(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///feedback");
    }
    public async void clicked_to_groups(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
    }
}
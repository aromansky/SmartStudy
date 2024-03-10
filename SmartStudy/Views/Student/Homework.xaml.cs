namespace SmartStudy.Views.Student;

public partial class Homework : ContentPage
{
	public Homework()
	{
		InitializeComponent();
        grid.SetColumnSpan(main_view, 2);
        Label lab = new Label();
        lab.Text = "«адани€ от учител€";
        Button btn_to_view_one_hw = new Button();
        btn_to_view_one_hw.Text = "посмотреть конкретное задание";
        btn_to_view_one_hw.Clicked += clicked_to_view_one_hw;
        main_view.Add(lab);
        main_view.Add(btn_to_view_one_hw);
#if WINDOWS
#else
        row_button.Height = 0;
#endif
    }
    public async void clicked_to_view_one_hw(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("view_one_hw");
    }
    public async void update_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///main_page");
        await Shell.Current.GoToAsync("///homework");
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
    /*
    private async void event_clicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            await Shell.Current.GoToAsync($"calendar_note_edit?note_id={((ModelsDB.Event)e.CurrentSelection[0]).event_id}");
            all_notes.SelectedItem = null;
        }
    }
    */
}
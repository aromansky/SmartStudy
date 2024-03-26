namespace SmartStudy.Views.Student;

public partial class Homework : ContentPage
{
	public Homework()
	{
		InitializeComponent();
        Label lab = new Label();
        lab.Text = "Задания от учителя";
        Button btn_to_view_one_hw = new Button();
        btn_to_view_one_hw.Text = "посмотреть конкретное задание";
        btn_to_view_one_hw.Clicked += clicked_to_view_one_hw;
        Button btn_to_list_all_hw_user = new Button();
        btn_to_list_all_hw_user.Text = "посмотреть все задания";
        btn_to_list_all_hw_user.Clicked += clicked_to_list_all_hw_user;
        Button btn_to_list_all_hw_one_group = new Button();
        btn_to_list_all_hw_one_group.Text = "посмотреть задания группы";
        btn_to_list_all_hw_one_group.Clicked += clicked_to_list_all_hw_one_group;
        main_view.Add(lab);
        main_view.Add(btn_to_view_one_hw);
        main_view.Add(btn_to_list_all_hw_user);
        main_view.Add(btn_to_list_all_hw_one_group);
#if WINDOWS
#else
        row_button.Height = 0;
#endif
    }
    public async void clicked_to_view_one_hw(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("view_one_hw");
    }
    public async void clicked_to_list_all_hw_user(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("list_all_hw_user");
    }
    public async void clicked_to_list_all_hw_one_group(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"list_all_hw_one_group?group_id={-1}");
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
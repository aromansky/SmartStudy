namespace SmartStudy.Views.Teacher;

public partial class Homework : ContentPage
{
	public Homework()
	{
		InitializeComponent();
        grid.SetColumnSpan(main_view, 2);
        Label lab = new Label();
        lab.Text = "Задания от учителя";
        Button btn_to_list_all_hw_user = new Button();
        btn_to_list_all_hw_user.Text = "Созданные домашние работы";
        btn_to_list_all_hw_user.Clicked += clicked_to_list_all_hw_user;
        Button btn_to_list_all_hw_group = new Button();
        btn_to_list_all_hw_group.Text = "Групповые домашние работы";
        btn_to_list_all_hw_group.Clicked += clicked_to_list_all_hw_group;
        main_view.Add(lab);
        main_view.Add(btn_to_list_all_hw_user);
        main_view.Add(btn_to_list_all_hw_group);
#if WINDOWS
#else
        row_button.Height = 0;
#endif
    }

    public async void clicked_to_create_task(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("add_hw");
    }
    public async void clicked_to_list_all_hw_user(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("list_all_hw_user");
    }
    public async void clicked_to_list_all_hw_group(object sender, EventArgs e)
    {
#if WINDOWS
        await Shell.Current.GoToAsync("list_all_hw_group");
#else
        await Shell.Current.GoToAsync("list_all_groups");
#endif
    }
    public async void update_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
        await Shell.Current.GoToAsync("///homework");
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
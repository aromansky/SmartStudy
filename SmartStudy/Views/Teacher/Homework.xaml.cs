namespace SmartStudy.Views.Teacher;

public partial class Homework : ContentPage
{
	public Homework()
	{
		InitializeComponent();
        grid.SetColumnSpan(main_view, 2);
        Label lab = new Label();
        lab.Text = "������� �� �������";
        Button btn_to_view_one_hw = new Button();
        btn_to_view_one_hw.Text = "���������� ���������� �������";
        btn_to_view_one_hw.Clicked += clicked_to_view_one_hw;
        Button btn_to_list_all_hw_user = new Button();
        btn_to_list_all_hw_user.Text = "���������� ��� ������� ������������";
        btn_to_list_all_hw_user.Clicked += clicked_to_list_all_hw_user;
        Button btn_to_list_all_hw_group = new Button();
        btn_to_list_all_hw_group.Text = "���������� ��� ������� ������������";
        btn_to_list_all_hw_group.Clicked += clicked_to_list_all_hw_group;
        main_view.Add(lab);
        main_view.Add(btn_to_view_one_hw);
        main_view.Add(btn_to_list_all_hw_user);
        main_view.Add(btn_to_list_all_hw_group);
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
    public async void clicked_to_list_all_hw_group(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("list_all_hw_group");
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
}
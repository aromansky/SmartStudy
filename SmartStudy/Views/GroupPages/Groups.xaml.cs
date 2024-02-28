using SmartStudy.Models;

namespace SmartStudy.Views.GroupPages;

public partial class Groups : ContentPage
{
	public Groups()
	{
		InitializeComponent();
        //grid.SetColumnSpan(main_view, 2);
        BindingContext = new Group_note();
#if WINDOWS
#else
        row_button.Height = 0;
#endif
    }
    public async void update_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///main_page");
        await Shell.Current.GoToAsync("///groups");
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

    public async void clicked_to_feedback(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///feedback");
    }

    public async void clicked_to_create_group(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("create_group");
    }

    protected override void OnAppearing()
    {
        ((Group_note)BindingContext).Load_Groups_With_User(Serializer.DeserializeUser().user_id);
        if (!Serializer.DeserializeUser().IsTutor())
        {
            CreateGroup.IsEnabled = false;
            CreateGroup.IsVisible = false;
        }

    }

    private async void group_ckicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            await Shell.Current.GoToAsync($"edit_group?group_settings_id={((ModelsDB.group_settings)e.CurrentSelection[0]).group_settings_id}");
            all_groups.SelectedItem = null;
        }
            
    }
}
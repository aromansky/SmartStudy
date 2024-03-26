using SmartStudy.Models;

namespace SmartStudy.Views.HomeworkPages;

public partial class List_all_hw_group : ContentPage
{
    public List_all_hw_group()
    {
        InitializeComponent();
        BindingContext = new Homework_list();
#if WINDOWS
#else
        row_button.Height = 0;
#endif
    }
    public async void update_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///main_page");
        await Shell.Current.GoToAsync("list_all_hw_group");
    }

    protected override void OnAppearing()
    {
        ((Homework_list)BindingContext).LoadHomework();
        if (!Serializer.DeserializeUser().IsTutor())
        {
            CreateGroup.IsEnabled = false;
            CreateGroup.IsVisible = false;
        }
    }

    private async void task_ckicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            //TODO
            //await Shell.Current.GoToAsync($"edit_group?group_settings_id={((ModelsDB.group_settings)e.CurrentSelection[0]).group_settings_id}");
            all_tasks.SelectedItem = null;
        }

    }

    public async void clicked_to_create_task(object sender, EventArgs e)
    {
        //TODO
        //await Shell.Current.GoToAsync("create_task");
    }


}
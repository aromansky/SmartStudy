using SmartStudy.Models;
using SmartStudy.Views.GroupPages;

namespace SmartStudy.Views.HomeworkPages;

public partial class List_all_hw_group : ContentPage
{
    public List_all_hw_group()
    {
        InitializeComponent();
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
        var list_groups = new Group_note();
        list_groups.Load_Groups_With_User(Serializer.DeserializeUser().user_id);
        all_groups.BindingContext = list_groups;
        if (!Serializer.DeserializeUser().IsTutor())
        {
            CreateHw.IsEnabled = false;
            CreateHw.IsVisible = false;
        }
    }

    private async void task_ckicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            //TODO
            await Shell.Current.GoToAsync($"view_one_hw?hw_id={((ModelsDB.homework)e.CurrentSelection[0]).homework_id}");
            all_tasks.SelectedItem = null;
        }

    }

    private void group_ckicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            var hw_list_now_group = new Homework_list();
            hw_list_now_group.LoadGroupHomework(((ModelsDB.group_settings)e.CurrentSelection[0]).group_settings_id);
            all_tasks.BindingContext = hw_list_now_group;
            all_groups.SelectedItem = null;
        }
    }

    public async void clicked_to_create_task(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("add_hw");
    }


}
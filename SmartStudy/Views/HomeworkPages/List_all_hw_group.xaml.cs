using SmartStudy.Models;
using SmartStudy.ModelsDB;

namespace SmartStudy.Views.HomeworkPages;

public partial class List_all_hw_group : ContentPage
{
    // Всё дз для групп
    public List_all_hw_group()
    {
        InitializeComponent();
        all_tasks.BindingContext = new Homework_list();
        groups.BindingContext = new Group_note();
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
        (groups.BindingContext as Group_note).Load_Groups_With_User();

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
            await Shell.Current.GoToAsync($"view_one_hw?hw_id={(e.CurrentSelection[0] as homework).homework_id}");
            all_tasks.SelectedItem = null;
        }
    }


    private async void group_clicked(object sender, SelectionChangedEventArgs e)
    {
        group_settings group = null;
        if (e.CurrentSelection.Count != 0)
        {
            group = e.CurrentSelection[0] as group_settings;
            UpdateHomework(group.group_settings_id);
        }
            
        

        if (!(group is null))
            label_hw.Text = $"Домашнее задание для {group.Title}";
    }

    public async void clicked_to_create_task(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("add_hw");
    }

    private async void remove_homework(object sender, EventArgs e)
    {
       long g_s_id = (groups.SelectedItem as group_settings).group_settings_id;
       await Client.DeleteGroupHomework(((sender as ImageButton).BindingContext as homework).homework_id, g_s_id);

       UpdateHomework(g_s_id);
    }

    private void UpdateHomework(long group_settings_id)
    {
        (all_tasks.BindingContext as Homework_list).LoadGroupHomework(group_settings_id);
    }

    private async void delete_homework(object sender, EventArgs e)
    {
        long g_s_id = (groups.SelectedItem as group_settings).group_settings_id;
        await Client.DeleteHomework(((sender as ImageButton).BindingContext as homework).homework_id);

        UpdateHomework(g_s_id);
    }
}
using SmartStudy.Models;
using SmartStudy.ModelsDB;

namespace SmartStudy.Views.HomeworkPages;

[QueryProperty(nameof(Group_get_Id), "group_id")]
public partial class List_all_hw_one_group : ContentPage
{
    private long group_id;
    public long Group_get_Id
    {
        set { group_id = value; }
    }
    public List_all_hw_one_group()
	{
		InitializeComponent();
        BindingContext = new Homework_list();
    }
    public async void homework_ckicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            await Shell.Current.GoToAsync($"view_one_hw?hw_id={((ModelsDB.homework)e.CurrentSelection[0]).homework_id}");
            all_homeworks_group.SelectedItem = null;
        }
    }

    protected override void OnAppearing()
    {
        (BindingContext as Homework_list).LoadGroupHomework(group_id);
    }

    private async void remove_homework(object sender, EventArgs e)
    {
        await Client.DeleteGroupHomework(((sender as ImageButton).BindingContext as homework).homework_id, group_id);

        UpdateHomework(group_id);
    }

    private void UpdateHomework(long group_settings_id)
    {
        (BindingContext as Homework_list).LoadGroupHomework(group_settings_id);
    }
}
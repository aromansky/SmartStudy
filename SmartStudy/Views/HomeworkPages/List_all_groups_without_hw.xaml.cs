using SmartStudy.Models;
using SmartStudy.ModelsDB;

namespace SmartStudy.Views.HomeworkPages;

[QueryProperty(nameof(Hw_get_Id), "hw_id")]
public partial class List_all_groups_without_hw : ContentPage
{
    private long hw_id;
    public long Hw_get_Id
    {
        set { hw_id = value; }
    }
    public List_all_groups_without_hw()
    {
        BindingContext = new Homework_list();
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        UpdateGroups(hw_id);
    }

    private void UpdateGroups(long group_settings_id)
    {
        (BindingContext as Homework_list).LoadGroupsWithoutHomework(hw_id);
    }

    private async void group_add(object sender, EventArgs e)
    {
        long g_s_id = ((sender as ImageButton).BindingContext as group_settings).group_settings_id;
        await Client.CreateGroupHomework(hw_id, g_s_id);

        UpdateGroups(hw_id);
    }
}
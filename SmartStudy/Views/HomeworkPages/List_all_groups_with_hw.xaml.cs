using SmartStudy.Models;
using SmartStudy.ModelsDB;

namespace SmartStudy.Views.HomeworkPages;

[QueryProperty(nameof(Hw_get_Id), "hw_id")]
public partial class List_all_groups_with_hw : ContentPage
{
    private long hw_id;
    public long Hw_get_Id
    {
        set { hw_id = value; }
    }
    public List_all_groups_with_hw()
	{
		InitializeComponent();
        BindingContext = new Homework_list();
    }

    protected override void OnAppearing()
    {
        ((Homework_list)BindingContext).LoadGroupsWithHomework(hw_id);
    }

    private async void remove_group(object sender, EventArgs e)
    {
        long g_s_id = ((sender as ImageButton).BindingContext as group_settings).group_settings_id;
        await Client.DeleteGroupHomework(hw_id, g_s_id);

        UpdateGroups(g_s_id);
    }

    private void UpdateGroups(long group_settings_id)
    {
        (BindingContext as Homework_list).LoadGroupsWithHomework(group_settings_id);
    }
}
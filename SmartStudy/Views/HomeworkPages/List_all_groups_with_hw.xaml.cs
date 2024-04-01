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
        UpdateGroups(hw_id);
    }

    private async void remove_group(object sender, EventArgs e)
    {
        long g_s_id = ((sender as ImageButton).BindingContext as group_settings).group_settings_id;
        await Client.DeleteGroupHomework(hw_id, g_s_id);

        UpdateGroups(hw_id);
    }

    private void UpdateGroups(long hw_id)
    {
        (BindingContext as Homework_list).LoadGroupsWithHomework(hw_id);
    }
}
using SmartStudy.Models;

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
        BindingContext = Client.GetGroupsWithHomework(hw_id);
    }
    private async void group_ckicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            await Shell.Current.GoToAsync($"edit_group?group_settings_id={((ModelsDB.group_settings)e.CurrentSelection[0]).group_settings_id}");
            all_groups_with_hw.SelectedItem = null;
        }
    }
}
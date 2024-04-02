using SmartStudy.Models;

namespace SmartStudy.Views.HomeworkPages;

public partial class List_all_groups : ContentPage
{

    public List_all_groups()
	{
		InitializeComponent();
        BindingContext = new Group_note();
    }
    private async void group_ckicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            await Shell.Current.GoToAsync($"list_all_hw_one_group?group_id={((ModelsDB.group_settings)e.CurrentSelection[0]).group_settings_id}");
            all_groups.SelectedItem = null;
        }
    }
    protected override void OnAppearing()
    {
        (BindingContext as Group_note).Load_Groups_With_User();
    }
}
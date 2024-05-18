using SmartStudy.Models;
using SmartStudy.ModelsDB;
using System.Globalization;

namespace SmartStudy.Views.FeedbackPages;

public partial class Groups_for_create_feedback : ContentPage
{
    List<long> groups;
    public Groups_for_create_feedback(ref List<long> groups)
	{
		InitializeComponent();
        BindingContext = new Group_note();
        this.groups = groups;
    }

    protected override void OnAppearing()
    {
        (BindingContext as Group_note).Load_Groups_With_User();
    }


    private void UpdateGroupss(group_settings group)
    {
        (BindingContext as Group_note).GroupsWithUser.Remove(group);
    }

    private async void add_user(object sender, EventArgs e)
    {
        group_settings group = (sender as ImageButton).BindingContext as group_settings;
        groups.Add(group.group_settings_id);
        UpdateGroupss(group);
    }
}

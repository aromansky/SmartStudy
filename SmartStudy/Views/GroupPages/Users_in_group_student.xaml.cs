using SmartStudy.Models;
using SmartStudy.ModelsDB;
using System.Globalization;

namespace SmartStudy.Views.GroupPages;

[QueryProperty(nameof(Group_get_Id), "group_id")]
public partial class Users_in_group_student : ContentPage
{
    private long group_id;
    public long Group_get_Id
    {
        set { group_id = value; }
    }
    public Users_in_group_student()
	{
		InitializeComponent();
        BindingContext = new User_list();
    }

    protected override void OnAppearing()
    {
        UpdateUsers(group_id);
    }


    private void UpdateUsers(long group_settings_id)
    {
        (BindingContext as User_list).Load_Users_In_Group(group_settings_id);
    }
}

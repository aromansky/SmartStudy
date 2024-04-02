using SmartStudy.Models;
using SmartStudy.ModelsDB;

namespace SmartStudy.Views.HomeworkPages;

[QueryProperty(nameof(Hw_get_Id), "hw_id")]
public partial class Users_with_hw : ContentPage
{
    private long hw_id;
    public long Hw_get_Id
    {
        set { hw_id = value; }
    }
    public Users_with_hw()
	{
		InitializeComponent();
        BindingContext = new User_list();
    }

    protected override void OnAppearing()
    {
        UpdateUsers(hw_id);
    }


    private void UpdateUsers(long hw_id)
    {
        (BindingContext as User_list).Load_Users_with_Homework(hw_id);
    }

    private async void remove_user(object sender, EventArgs e)
    {
        await Client.DeleteUserHomework(hw_id, ((sender as ImageButton).BindingContext as User).user_id);
        UpdateUsers(hw_id);
    }
}

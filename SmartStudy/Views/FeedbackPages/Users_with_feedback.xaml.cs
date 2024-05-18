using SmartStudy.Models;
using SmartStudy.ModelsDB;
using System.Globalization;

namespace SmartStudy.Views.FeedbackPages;

public partial class Users_with_feedback : ContentPage
{
    public long feedback_id;
    public Users_with_feedback(long feedback_id)
	{
        this.feedback_id = feedback_id;
        InitializeComponent();
        BindingContext = new User_list();
    }

    protected override async void OnAppearing()
    {
        (BindingContext as User_list).Load_Users_With_Feedback(feedback_id);
    }
}

using SmartStudy.ModelsDB;
using SmartStudy.Models;
using System.Globalization;

namespace SmartStudy.Views.FeedbackPages;

[QueryProperty(nameof(Feedback_get_Id), "feedback_id")]
public partial class Edit_feedback : ContentPage
{
    long feedback_id;
    public long Feedback_get_Id
    {
        set { Load_feedback__Id(value); }   
    }
    private async void Load_feedback__Id(long text_obj)
    {
        feedback_id = text_obj;
        feedback feedback = await Client.GetFeedbackForID(feedback_id);
        Title.Text = feedback.Title;
        Description.Text = feedback.Description;
        User user = await Client.GetUserForID(feedback.author_id);
        Author.Text = $"Преподаватель: {user.LastName} {user.FirstName}";
    }

    public Edit_feedback()
	{
		InitializeComponent();
    }
    protected override void OnAppearing()
    {
        if (!Serializer.DeserializeUser().IsTutor())
        {
            EditButton.IsEnabled = false;
            EditButton.IsVisible = false;

            DeleteButton.IsEnabled = false;
            DeleteButton.IsVisible = false;
        }
    }
}
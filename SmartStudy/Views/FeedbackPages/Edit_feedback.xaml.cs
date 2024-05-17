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

    private void EditButton_Clicked(object sender, EventArgs e)
    {
        Title.IsReadOnly = false;
        Description.IsReadOnly = false;
        SaveButton.IsEnabled = true;
        SaveButton.IsVisible = true;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        Client.EditFeedbackFromId(feedback_id, new feedback(Serializer.DeserializeUser().user_id,
            Title.Text, Description.Text));
        await Shell.Current.GoToAsync("///feedback");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Подтвердить действие", $"Вы хотите удалить данный фидбек?", "Да", "Нет");
        if (result)
        {
            await Client.DeleteFeedback(feedback_id);
            await Shell.Current.GoToAsync("///feedback");
        }
    }
}
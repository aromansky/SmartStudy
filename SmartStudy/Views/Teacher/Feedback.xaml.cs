using SmartStudy.Models;

namespace SmartStudy.Views.Teacher;

public partial class Feedback : ContentPage
{
    public Feedback()
    {
        InitializeComponent();
        BindingContext = new Feedback_list();
#if WINDOWS
#else
        row_button.Height = 0;
#endif
    }
    public async void update_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///main_page");
        await Shell.Current.GoToAsync("///feedback");
    }
    public async void clicked_to_main_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///main_page");
    }
    public async void clicked_to_calendar(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///calendar");
    }
    public async void clicked_to_homework(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///homework");
    }
    public async void clicked_to_groups(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
    }

    public async void clicked_to_create_feedback(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("create_feedback");
    }


    protected override void OnAppearing()
    {
        (BindingContext as Feedback_list).LoadUserFeedback();
        if (!Serializer.DeserializeUser().IsTutor())
        {
            CreateFeedback.IsEnabled = false;
            CreateFeedback.IsVisible = false;
        }
    }
    private async void feedback_clicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            await Shell.Current.GoToAsync($"edit_feedback?feedback_id={((ModelsDB.feedback)e.CurrentSelection[0]).feedback_id}");
            all_feedbacks.SelectedItem = null;
        }
    }
}
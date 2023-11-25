using SmartStudy.Models;

namespace SmartStudy.Views.CalendarPages;

public partial class Calendar : ContentPage
{
	public Calendar()
	{
		InitializeComponent();
        BindingContext = new Calendar_note();
#if WINDOWS
#else
        row_button.Height = 0;
#endif
        }
    public async void clicked_to_main_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///main_page");
    }
    public async void clicked_to_groups(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
    }
    public async void clicked_to_feedback(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///feedback");
    }
    public async void clicked_to_homework(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///homework");
    }
    public async void clicked_event_add(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("calendar_note_add");
    }
    protected override void OnAppearing()
    {
        ((Calendar_note)BindingContext).Load_All_Events();
        if (!Serializer.DeserializeUser().IsTutor())
        {
            AddEvent.IsEnabled = false;
            AddEvent.IsVisible = false;
        }
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Calendar_note_add));
    }

    private async void event_clicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            var @event = (ModelsDB.Event)e.CurrentSelection[0];

            await Shell.Current.GoToAsync($"calendar_note_edit?note_id={@event.event_id}");
            all_notes.SelectedItem = null;
        }
    }
}
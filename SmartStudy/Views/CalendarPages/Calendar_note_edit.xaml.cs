using SmartStudy.Models;
using SmartStudy.ModelsDB;

namespace SmartStudy.Views.CalendarPages;

[QueryProperty(nameof(Note_get_Id), "note_id")]
public partial class Calendar_note_edit : ContentPage
{
    long event_id;
    string title_event;
    string descr_event;
    DateTime date_begin_note;
    DateTime date_end_note;
    Event @event = null;
    User user = Serializer.DeserializeUser();
    public long Note_get_Id
    {
        set { LoadNote_id(value); }
    }
    public Calendar_note_edit()
	{
		InitializeComponent();
        BindingContext = new Group_note();
    }
    private async void LoadNote_id(long text_obj)
    {
        event_id = text_obj;
        @event = await Client.GetEventFromId(event_id);
        title_event = @event.Title;
        descr_event = @event.Description;
        date_begin_note = @event.date_begin;
        date_end_note = @event.date_end;

        Note_Name_entry.Text = title_event;
        TextEditor.Text = descr_event;
        all_date_begin.Text = date_begin_note.ToString("g");
        date_change_begin.Date = date_begin_note;
        time_change_begin.Time = date_begin_note.TimeOfDay;
        all_date_end.Text = date_end_note.ToString("g");
        date_change_end.Date = date_end_note;
        time_change_end.Time = date_end_note.TimeOfDay;

        if(user.user_id != @event.author_id)
        {
            Edit_button.IsEnabled = false;
            Edit_button.IsVisible = false;
        }
        
    }
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        Calendar_note calendar_Note = new Calendar_note();
        //title_event = Note_Name_entry.Text;
        //descr_event = TextEditor.Text;
        //date_begin_note = DateTime.ParseExact(date_change_begin.Date.ToString("dd.MM.yyyy") + " " +
        //    time_change_begin.Time.ToString("hh\\:mm"), "g", null);
        //date_end_note = DateTime.ParseExact(date_change_end.Date.ToString("dd.MM.yyyy") + " " +
        //    time_change_end.Time.ToString("hh\\:mm"), "g", null);
        if (time_change_begin.Time > time_change_end.Time)
        {
            DisplayAlert("Ошибка", "Неверное время окончания", "ОК");
            time_change_end.Time = time_change_begin.Time;
            return;
        }
        calendar_Note.Save_edit_note(event_id, Note_Name_entry.Text, TextEditor.Text,
            DateTime.ParseExact(date_change_begin.Date.ToString("dd.MM.yyyy") + " " +
            time_change_begin.Time.ToString("hh\\:mm"), "g", null),
            DateTime.ParseExact(date_change_end.Date.ToString("dd.MM.yyyy") + " " +
            time_change_end.Time.ToString("hh\\:mm"), "g", null));

        await Shell.Current.GoToAsync("///calendar");
    }
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Подтвердить действие", $"Вы хотите удалить событие {title_event}?", "Да", "Нет");
        if (result)
        {
            Models.Calendar_note calendar_Note = new Models.Calendar_note();
            calendar_Note.Delete_note(event_id);
            await Shell.Current.GoToAsync("///calendar");
        }
    }
    public void date_begin_selected(object sender, DateChangedEventArgs e)
    {
        all_date_begin.Text = e.NewDate.ToString("dd.MM.yyyy") + " " + time_change_begin.Time.ToString("hh\\:mm");
        date_change_end.MinimumDate = date_change_begin.Date;
    }
    public void time_begin_selected(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Time")
        {
            all_date_begin.Text = date_change_begin.Date.ToString("dd.MM.yyyy") + " " + time_change_begin.Time.ToString("hh\\:mm");
            if (time_change_begin.Time > time_change_end.Time)
                time_change_end.Time = time_change_begin.Time;
        }
    }
    public void date_end_selected(object sender, DateChangedEventArgs e)
    {
        all_date_end.Text = e.NewDate.ToString("dd.MM.yyyy") + " " + time_change_end.Time.ToString("hh\\:mm");
    }
    public void time_end_selected(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Time")
            all_date_end.Text = date_change_end.Date.ToString("dd.MM.yyyy") + " " + $"{time_change_end.Time.ToString("hh\\:mm")}";
    }
    public void Edit_clicked(object sender, EventArgs e)
    {
        GroupsWithEvent.IsVisible = false;
        GroupsWithEvent.IsEnabled = true;
        Note_Name_entry.IsReadOnly = false;
        TextEditor.IsReadOnly = false;
        date_change_begin.IsEnabled = true;
        time_change_begin.IsEnabled = true;
        date_change_end.IsEnabled = true;
        time_change_end.IsEnabled = true;
        Add_group_in_event.IsVisible = true;
        Save_button.IsVisible = true;
        Cancel_button.IsVisible = true;
        Edit_button.IsVisible = false;
        Delete_button.IsVisible = false;
    }
    public async void Add_group_in_event_clicked(object sender, EventArgs e)
    {
        Calendar_note calendar_Note = new Calendar_note();
        //// костыль
        //Event ev = new Event(Note_Name_entry.Text, Serializer.DeserializeUser().user_id, TextEditor.Text,
        //    DateTime.ParseExact(date_change_begin.Date.ToString("dd.MM.yyyy") + " " +
        //    time_change_begin.Time.ToString("hh\\:mm"), "g", null),
        //    DateTime.ParseExact(date_change_end.Date.ToString("dd.MM.yyyy") + " " +
        //    time_change_end.Time.ToString("hh\\:mm"), "g", null));
        //ev.event_id = event_id;

        //calendar_Note.Save_edit_note(event_id, Note_Name_entry.Text, TextEditor.Text,
        //    DateTime.ParseExact(date_change_begin.Date.ToString("dd.MM.yyyy") + " " +
        //    time_change_begin.Time.ToString("hh\\:mm"), "g", null),
        //    DateTime.ParseExact(date_change_end.Date.ToString("dd.MM.yyyy") + " " +
        //    time_change_end.Time.ToString("hh\\:mm"), "g", null));
        //// костыль
        //Serializer.SerializeEvent(ev);
        await Shell.Current.GoToAsync($"///add_event_to_group?note_id={@event.event_id}");
    }
    public void Cancel_button_clicked(object sender, EventArgs e)
    {
        Note_Name_entry.IsReadOnly = true;
        TextEditor.IsReadOnly = true;
        date_change_begin.IsEnabled = false;
        time_change_begin.IsEnabled = false;
        date_change_end.IsEnabled = false;
        time_change_end.IsEnabled = false;
        Add_group_in_event.IsVisible = false;
        Save_button.IsVisible = false;
        Cancel_button.IsVisible = false;
        Edit_button.IsVisible = true;
        Delete_button.IsVisible = true;
        Note_Name_entry.Text = title_event;
        all_date_begin.Text = date_begin_note.ToString("g");
        date_change_begin.Date = date_begin_note;
        time_change_begin.Time = date_begin_note.TimeOfDay;
        all_date_end.Text = date_end_note.ToString("g");
        date_change_end.Date = date_end_note;
        time_change_end.Time = date_end_note.TimeOfDay;
        TextEditor.Text = descr_event;
    }

    protected override void OnAppearing()
    {
        if (!user.IsTutor())
        {
            Cancel_button.IsEnabled = false;
            Cancel_button.IsVisible = false;

            Delete_button.IsEnabled = false;
            Delete_button.IsVisible = false;
        }
        ((Group_note)BindingContext).Load_Groups_With_Event(event_id);
    }
}
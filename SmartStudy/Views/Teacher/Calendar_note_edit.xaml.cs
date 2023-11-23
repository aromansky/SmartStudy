using SmartStudy.Models;
using System.Collections.ObjectModel;

namespace SmartStudy.Views.Teacher;

[QueryProperty(nameof(Note_get_Id), "note_id")]
public partial class Calendar_note_edit : ContentPage
{
    long note_id;
    string name_note;
    string text_note;
    DateTime date_begin_note;
    DateTime date_end_note;
    public long Note_get_Id
    {
        set { LoadNote_id(value); }
    }
    public Calendar_note_edit()
	{
		InitializeComponent();
    }
    private void LoadNote_id(long text_obj)
    {
        note_id = text_obj;
        Models.Calendar_note calendar_Note = new Models.Calendar_note();
        ModelsDB.Event @event = new ModelsDB.Event();
        @event = calendar_Note.Get_Note_By_Id(note_id);
        name_note = @event.Title;
        text_note = @event.Description;
        date_begin_note = @event.date_begin;
        date_end_note = @event.date_end;

        Note_Name_entry.Text = name_note;
        TextEditor.Text = text_note;
        all_date_begin.Text = date_begin_note.ToString("g");
        date_change_begin.Date = date_begin_note;
        time_change_begin.Time = date_begin_note.TimeOfDay;
        all_date_end.Text = date_end_note.ToString("g");
        date_change_end.Date = date_end_note;
        time_change_end.Time = date_end_note.TimeOfDay;
    }
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        Models.Calendar_note calendar_Note = new Models.Calendar_note();
        name_note = Note_Name_entry.Text;
        text_note = TextEditor.Text;
        date_begin_note = DateTime.ParseExact(date_change_begin.Date.ToString("dd.MM.yyyy") + " " + 
            time_change_begin.Time.ToString("hh\\:mm"), "g", null);
        date_end_note = DateTime.ParseExact(date_change_end.Date.ToString("dd.MM.yyyy") + " " +
            time_change_end.Time.ToString("hh\\:mm"), "g", null);
        calendar_Note.Save_edit_note(note_id, name_note, text_note, date_begin_note, date_end_note);
        Cancel_button_clicked(Cancel_button, EventArgs.Empty);
    }
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Подтвердить действие", $"Вы хотите удалить событие {name_note}?", "Да", "Нет");
        if (result)
        {
            Models.Calendar_note calendar_Note = new Models.Calendar_note();
            calendar_Note.Delete_note(note_id);
            await Shell.Current.GoToAsync("///calendar");
        }
    }
    public void date_begin_selected(object sender, DateChangedEventArgs e)
    {
        all_date_begin.Text = e.NewDate.ToString("dd.MM.yyyy") + " " + time_change_begin.Time.ToString("hh\\:mm");
    }
    public void time_begin_selected(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Time")
            all_date_begin.Text = date_change_begin.Date.ToString("dd.MM.yyyy") + " " + time_change_begin.Time.ToString("hh\\:mm");
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
        Note_Name_entry.IsReadOnly = false;
        TextEditor.IsReadOnly = false;
        date_change_begin.IsEnabled = true;
        time_change_begin.IsEnabled = true;
        date_change_end.IsEnabled = true;
        time_change_end.IsEnabled = true;
        Add_user.IsVisible = true;
        Save_button.IsVisible = true;
        Cancel_button.IsVisible = true;
        Edit_button.IsVisible = false;
        Delete_button.IsVisible = false;
    }
    public void Add_user_clicked(object sender, EventArgs e)
    {
        //
    }
    public void Cancel_button_clicked(object sender, EventArgs e)
    {
        Note_Name_entry.IsReadOnly = true;
        TextEditor.IsReadOnly = true;
        date_change_begin.IsEnabled = false;
        time_change_begin.IsEnabled = false;
        date_change_end.IsEnabled = false;
        time_change_end.IsEnabled = false;
        Add_user.IsVisible = false;
        Save_button.IsVisible = false;
        Cancel_button.IsVisible = false;
        Edit_button.IsVisible = true;
        Delete_button.IsVisible = true;
        Note_Name_entry.Text = name_note;
        all_date_begin.Text = date_begin_note.ToString("g");
        date_change_begin.Date = date_begin_note;
        time_change_begin.Time = date_begin_note.TimeOfDay;
        all_date_end.Text = date_end_note.ToString("g");
        date_change_end.Date = date_end_note;
        time_change_end.Time = date_end_note.TimeOfDay;
        TextEditor.Text = text_note;
    }
}
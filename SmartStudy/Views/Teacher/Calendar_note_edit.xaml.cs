using SmartStudy.Models;
using System.Collections.ObjectModel;

namespace SmartStudy.Views.Teacher;

[QueryProperty(nameof(Note_get_Id), "note_id")]
[QueryProperty(nameof(Name_get_note), "name_note")]
[QueryProperty(nameof(Date_get_note), "date_note")]
[QueryProperty(nameof(Time_get_note), "time_note")]
[QueryProperty(nameof(Text_get_note), "text_note")]
public partial class Calendar_note_edit : ContentPage
{
    int note_id;
    string name_note;
    string date_note;
    string time_note;
    string text_note;
    public int Note_get_Id
    {
        set { LoadNote_id(value); }
    }
    public string Name_get_note
    {
        set { LoadNote_name(value); }
    }
    public string Date_get_note
    {
        set { LoadNote_date(value); }
    }
    public string Time_get_note
    {
        set { LoadNote_time(value); }
    }
    public string Text_get_note
    {
        set { LoadNote_text(value); }
    }
    public Calendar_note_edit()
	{
		InitializeComponent();
    }
    private void LoadNote_id(int text_obj)
    {
        note_id = text_obj;
    }
    private void LoadNote_name(string text_obj)
    {
        name_note = text_obj;
        Note_Name_entry.Text = name_note;
    }
    private void LoadNote_date(string text_obj)
    {
        date_note = text_obj;
        all_date.Text = text_obj;
        date_change.Date = DateTime.ParseExact(date_note, "dd.MM.yyyy", null);
    }
    private void LoadNote_time(string text_obj)
    {
        time_note = text_obj;
        all_date.Text += " " + text_obj;
        time_change.Time = TimeSpan.ParseExact(time_note, "hh\\:mm", null);
    }
    private void LoadNote_text(string text_obj)
    {
        text_note = text_obj;
        TextEditor.Text = text_note;
    }
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        Models.Calendar_note calendar_Note = new Models.Calendar_note();
        name_note = Note_Name_entry.Text;
        date_note = date_change.Date.ToString("dd.MM.yyyy");
        time_note = time_change.Time.ToString("hh\\:mm");
        text_note = TextEditor.Text;
        calendar_Note.Save_edit_note(note_id, Note_Name_entry.Text, date_change.Date.ToString("dd.MM.yyyy"),
            time_change.Time.ToString("hh\\:mm"), TextEditor.Text);
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
    public void date_selected(object sender, DateChangedEventArgs e)
    {
        all_date.Text = e.NewDate.ToString("dd.MM.yyyy") + " " + time_change.Time.ToString("hh\\:mm");
    }
    public void time_selected(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Time")
            all_date.Text = date_change.Date.ToString("dd.MM.yyyy") + " " + time_change.Time.ToString("hh\\:mm");
    }
    public void Edit_clicked(object sender, EventArgs e)
    {
        Note_Name_entry.IsReadOnly = false;
        TextEditor.IsReadOnly = false;
        date_change.IsEnabled = true;
        time_change.IsEnabled = true;
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
        date_change.IsEnabled = false;
        time_change.IsEnabled = false;
        Add_user.IsVisible = false;
        Save_button.IsVisible = false;
        Cancel_button.IsVisible = false;
        Edit_button.IsVisible = true;
        Delete_button.IsVisible = true;
        Note_Name_entry.Text = name_note;
        all_date.Text = date_note + " " + time_note;
        date_change.Date = DateTime.ParseExact(date_note, "dd.MM.yyyy", null);
        time_change.Time = TimeSpan.ParseExact(time_note, "hh\\:mm", null);
        TextEditor.Text = text_note;
    }
}
namespace SmartStudy.Views.Teacher;

public partial class Calendar_note_add : ContentPage
{
    public Calendar_note_add()
    {
        InitializeComponent();
    }
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        Models.Calendar_note calendar_Note = new Models.Calendar_note();
        calendar_Note.add_data(Note_Name_entry.Text, date_change.Date.ToString("d"),
            time_change.Time.ToString(), TextEditor.Text);
        await Shell.Current.GoToAsync("///calendar");
    }
    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///calendar");
    }
    public void date_selected(object sender, DateChangedEventArgs e)
    {
        all_date.Text = e.NewDate.ToString("d") + " " + time_change.Time;
        
    }
    public void time_selected(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Time")
            all_date.Text = date_change.Date.ToString("d") + " " + $"{time_change.Time}";
    }
}
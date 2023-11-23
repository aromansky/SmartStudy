namespace SmartStudy.Views.Student;

public partial class Calendar_note_add : ContentPage
{
    public Calendar_note_add()
    {
        InitializeComponent();
    }
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        Models.Calendar_note calendar_Note = new Models.Calendar_note();
        calendar_Note.add_data(Title.Text, Description.Text,
            DateTime.ParseExact(date_change_begin.Date.ToString("dd.MM.yyyy") + " " +
            time_change_begin.Time.ToString("hh\\:mm"), "g", null),
            DateTime.ParseExact(date_change_end.Date.ToString("dd.MM.yyyy") + " " +
            time_change_end.Time.ToString("hh\\:mm"), "g", null));
        await Shell.Current.GoToAsync("///calendar");
    }
    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///calendar");
    }
    public void date_begin_selected(object sender, DateChangedEventArgs e)
    {
        all_date_begin.Text = e.NewDate.ToString("dd.MM.yyyy") + " " + time_change_begin.Time.ToString("hh\\:mm");
    }
    public void time_begin_selected(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Time")
            all_date_begin.Text = date_change_begin.Date.ToString("dd.MM.yyyy") + " " + $"{time_change_begin.Time.ToString("hh\\:mm")}";
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
}
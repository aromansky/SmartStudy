using SmartStudy.ModelsDB;
using SmartStudy.Models;

namespace SmartStudy.Views.HomeworkPages;

public partial class Add_hw : ContentPage
{
	public Add_hw()
	{
        InitializeComponent();
	}
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (time_change_begin.Time > time_change_end.Time)
        {
            DisplayAlert("Ошибка", "Неверное время окончания", "ОК");
            time_change_end.Time = time_change_begin.Time;
            return;
        }
        if (String.IsNullOrEmpty(Title.Text))
        {
            DisplayAlert("Ошибка", "Введите название дз", "ОК");
            return;
        }
        Client.CreateHomework(new ModelsDB.homework(Title.Text, Description.Text, Serializer.DeserializeUser().user_id,
            DateTime.ParseExact(date_change_begin.Date.ToString("dd.MM.yyyy") + " " + time_change_begin.Time.ToString("hh\\:mm"), "g", null),
            DateTime.ParseExact(date_change_end.Date.ToString("dd.MM.yyyy") + " " + time_change_end.Time.ToString("hh\\:mm"), "g", null)));
        await Shell.Current.GoToAsync("///homework");
    }
    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///homework");
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
            all_date_begin.Text = date_change_begin.Date.ToString("dd.MM.yyyy") + " " + $"{time_change_begin.Time.ToString("hh\\:mm")}";
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
    public async void add_group(object sender, EventArgs e)
    {
        // TODO: переход на добавление групп в дз

    }
}
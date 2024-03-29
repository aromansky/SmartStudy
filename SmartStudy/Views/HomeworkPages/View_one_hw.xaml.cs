using SmartStudy.Models;
using SmartStudy.ModelsDB;

namespace SmartStudy.Views.HomeworkPages;

[QueryProperty(nameof(Hw_get_Id), "hw_id")]
public partial class View_one_hw : ContentPage
{
    User user = Serializer.DeserializeUser();
    string title_hw;
    string descr_hw;
    long hw_id;
    DateTime date_begin_hw;
    DateTime date_end_hw;

    public long Hw_get_Id
    {
        set { Load_Hw(value); }
    }
    public View_one_hw()
	{
		InitializeComponent();
        // TODO: аналогично для дз
        //BindingContext = new Group_note();
    }
    public void Load_Hw(long hw_id)
    {
        // TODO: загружаем поля задания в переменные по id дз
        string title_hw = "title" + hw_id.ToString();
        string descr_hw = "description";
        DateTime date_begin_hw = new DateTime(2024, 3, 25, 18, 30, 0);
        DateTime date_end_hw = new DateTime(2024, 3, 26, 20, 0, 0);

        this.hw_id = hw_id;
        this.title_hw = title_hw;
        this.date_begin_hw = date_begin_hw;
        this.date_end_hw= date_end_hw;
        this.descr_hw = descr_hw;

        all_date_begin.Text = date_begin_hw.ToString("g");
        date_change_begin.Date = date_begin_hw;
        time_change_begin.Time = date_begin_hw.TimeOfDay;
        all_date_end.Text = date_end_hw.ToString("g");
        date_change_end.Date = date_end_hw;
        time_change_end.Time = date_end_hw.TimeOfDay;
        Title.Text = title_hw;
        Description.Text = descr_hw;

        // TODO: раскоментировать при написании функции получения id автора по id дз
        /*if (user.user_id != homework.author_id)
        {
            Edit_button.IsEnabled = false;
            Edit_button.IsVisible = false;
        }*/
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
        // TODO: добавление задания в список
        await Shell.Current.GoToAsync("///homework");
    }
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Подтвердить действие", $"Вы хотите удалить дз {title_hw}?", "Да", "Нет");
        if (result)
        {
            // TODO: функция удаления hw
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
        GroupsWithHw.IsVisible = false;
        GroupsWithHw.IsEnabled = true;
        Title.IsReadOnly = false;
        Description.IsReadOnly = false;
        date_change_begin.IsEnabled = true;
        time_change_begin.IsEnabled = true;
        date_change_end.IsEnabled = true;
        time_change_end.IsEnabled = true;
        Add_group_in_hw.IsVisible = true;
        Save_button.IsVisible = true;
        Cancel_button.IsVisible = true;
        Edit_button.IsVisible = false;
        Delete_button.IsVisible = false;
    }
    public /*async*/ void Add_group_in_event_clicked(object sender, EventArgs e)
    {
        // TODO: функция добавления группы в дз
    }
    public void Cancel_button_clicked(object sender, EventArgs e)
    {
        Title.IsReadOnly = true;
        Description.IsReadOnly = true;
        date_change_begin.IsEnabled = false;
        time_change_begin.IsEnabled = false;
        date_change_end.IsEnabled = false;
        time_change_end.IsEnabled = false;
        Add_group_in_hw.IsVisible = false;
        Save_button.IsVisible = false;
        Cancel_button.IsVisible = false;
        Edit_button.IsVisible = true;
        Delete_button.IsVisible = true;
        Title.Text = title_hw;
        all_date_begin.Text = date_begin_hw.ToString("g");
        date_change_begin.Date = date_begin_hw;
        time_change_begin.Time = date_begin_hw.TimeOfDay;
        all_date_end.Text = date_end_hw.ToString("g");
        date_change_end.Date = date_end_hw;
        time_change_end.Time = date_end_hw.TimeOfDay;
        Description.Text = descr_hw;
    }

    protected override void OnAppearing()
    {
        if (!user.IsTutor())
        {
            Cancel_button.IsEnabled = false;
            Cancel_button.IsVisible = false;

            Delete_button.IsEnabled = false;
            Delete_button.IsVisible = false;

            Edit_button.IsEnabled = false;
            Edit_button.IsVisible = false;
        }
        // TODO: аналогично для групп с дз
        //((Group_note)BindingContext).Load_Groups_With_Event(event_id);
    }
}
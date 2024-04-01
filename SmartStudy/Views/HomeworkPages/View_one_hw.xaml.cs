using SmartStudy.Models;
using SmartStudy.ModelsDB;

namespace SmartStudy.Views.HomeworkPages;

[QueryProperty(nameof(Hw_get_Id), "hw_id")]
public partial class View_one_hw : ContentPage
{
    User user = Serializer.DeserializeUser();
    homework homework;

    public long Hw_get_Id
    {
        set { Load_Hw(value); }
    }
    public View_one_hw()
    {
        InitializeComponent();
        BindingContext = new Homework_list();

#if WINDOWS

#else
        if(user.IsTutor())
        {
            ToolbarItem add_group = new ToolbarItem { IconImageSource = ImageSource.FromFile("group_add_white.svg") };
            ToolbarItem groups = new ToolbarItem { IconImageSource = ImageSource.FromFile("groups1.svg") };

            add_group.Clicked += Add_group_Clicked;
            groups.Clicked += Groups_with_hw_Clicked;

            if (this.ToolbarItems.Count == 0)
            {
                this.ToolbarItems.Add(add_group);
                this.ToolbarItems.Add(groups);
            }
        }  
#endif
    }
    public async void Load_Hw(long hw_id)
    {
        homework = await Client.GetHomeworkFromId(hw_id);
        all_date_begin.Text = homework.date_begin.ToString("g");
        date_change_begin.Date = homework.date_begin;
        time_change_begin.Time = homework.date_begin.TimeOfDay;
        all_date_end.Text = homework.date_end.ToString("g");
        date_change_end.Date = homework.date_end;
        time_change_end.Time = homework.date_end.TimeOfDay;
        Title.Text = homework.Title;
        Description.Text = homework.Description;

        if (user.user_id != homework.author_id)
        {
            Edit_button.IsEnabled = false;
            Edit_button.IsVisible = false;
        }
    }

    private async void Add_group_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"list_all_groups_without_hw?hw_id={homework.homework_id}");
    }

    private async void Groups_with_hw_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"list_all_groups_with_hw?hw_id={homework.homework_id}");
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
        Client.EditHomeworkFromId(homework.homework_id, new ModelsDB.homework(Title.Text, Description.Text, Serializer.DeserializeUser().user_id,
            DateTime.ParseExact(date_change_begin.Date.ToString("dd.MM.yyyy") + " " + time_change_begin.Time.ToString("hh\\:mm"), "g", null),
            DateTime.ParseExact(date_change_end.Date.ToString("dd.MM.yyyy") + " " + time_change_end.Time.ToString("hh\\:mm"), "g", null)));
        await Shell.Current.GoToAsync("///homework");
    }
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Подтвердить действие", $"Вы хотите удалить дз {homework.Title}?", "Да", "Нет");
        if (result)
        {
            await Client.DeleteHomework(homework.homework_id);
            await Shell.Current.GoToAsync("///homework");
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
        //GroupsWithHw.IsVisible = false;
        //GroupsWithHw.IsEnabled = true;
        Title.IsReadOnly = false;
        Description.IsReadOnly = false;
        date_change_begin.IsEnabled = true;
        time_change_begin.IsEnabled = true;
        date_change_end.IsEnabled = true;
        time_change_end.IsEnabled = true;
        //Add_group_in_hw.IsVisible = true;
        Save_button.IsVisible = true;
        Cancel_button.IsVisible = true;
        Edit_button.IsVisible = false;
        Delete_button.IsVisible = false;
    }
    public /*async*/ void Add_homework_to_group_clicked(object sender, EventArgs e)
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
        //Add_group_in_hw.IsVisible = false;
        Save_button.IsVisible = false;
        Cancel_button.IsVisible = false;
        Edit_button.IsVisible = true;
        Delete_button.IsVisible = true;
        Title.Text = homework.Title;
        all_date_begin.Text = homework.date_begin.ToString("g");
        date_change_begin.Date = homework.date_begin;
        time_change_begin.Time = homework.date_begin.TimeOfDay;
        all_date_end.Text = homework.date_end.ToString("g");
        date_change_end.Date = homework.date_end;
        time_change_end.Time = homework.date_end.TimeOfDay;
        Description.Text = homework.Description;
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

        ((Homework_list)BindingContext).LoadHomework();
    }
}
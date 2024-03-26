namespace SmartStudy.Views.HomeworkPages;

[QueryProperty(nameof(Hw_get_Id), "hw_id")]
public partial class View_one_hw : ContentPage
{
    public long Hw_get_Id
    {
        set { Load_Hw(value); }
    }
    public View_one_hw()
	{
		InitializeComponent();
    }
    public void Load_Hw(long hw_id)
    {
        // TODO: загружаем поля задания в переменные по id дз
        string title_hw = "title" + hw_id.ToString();
        string descr_hw = "description";
        DateTime date_begin_hw = new DateTime(2024, 3, 25, 18, 30, 0);
        DateTime date_end_hw = new DateTime(2024, 3, 26, 20, 0, 0);
        
        all_date_begin.Text = date_begin_hw.ToString("g");
        date_change_begin.Date = date_begin_hw;
        time_change_begin.Time = date_begin_hw.TimeOfDay;
        all_date_end.Text = date_end_hw.ToString("g");
        date_change_end.Date = date_end_hw;
        time_change_end.Time = date_end_hw.TimeOfDay;
        Title.Text = title_hw;
        Description.Text = descr_hw;
    }
}
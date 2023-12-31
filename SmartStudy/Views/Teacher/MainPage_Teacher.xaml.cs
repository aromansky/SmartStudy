using Microsoft.Maui.Controls;

namespace SmartStudy.Views.Teacher;

public partial class MainPage_Teacher : ContentPage
{    
    public MainPage_Teacher()
    {
        InitializeComponent();
        grid.SetColumnSpan(main_view, 2);
        Label lab = new Label();
        lab.Text = "������� �������� �������������";
        main_view.Add(lab);
#if WINDOWS
#else
        row_button.Height = 0;
#endif
    }
    public async void clicked_to_calendar(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///calendar");
    }
    public async void clicked_to_groups(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
    }
    public async void clicked_to_homework(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///homework");
    }
    public async void clicked_to_feedback(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///feedback");
    }
}
using Microsoft.Maui.Controls;
using SmartStudy.Models;

namespace SmartStudy.Views.Teacher;

public partial class MainPage_Teacher : ContentPage
{    
    public MainPage_Teacher()
    {
        InitializeComponent();
        grid.SetColumnSpan(main_view, 2);
        Label lab = new Label();
        lab.Text = "Главная страница преподавателя";
        main_view.Add(lab);
#if WINDOWS
#else
        row_button.Height = 0;
#endif
    }
    public async void update_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///feedback");
        await Shell.Current.GoToAsync("///main_page");
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
    public async void logOut(object sender, EventArgs e)
    {
        Serializer.DeleteUserData();
        Application.Current.MainPage = new AppShell();
        await Navigation.PopToRootAsync();
        await Shell.Current.GoToAsync("///RegistrationPage");
    }

}
using Microsoft.Maui.Controls;

namespace SmartStudy.Views.Teacher;

public partial class AppShell_Teacher : Shell
{
    public AppShell_Teacher()
    {
        InitializeComponent();
        Routing.RegisterRoute("main_page", typeof(MainPage_Teacher));
        Routing.RegisterRoute("calendar", typeof(Calendar));
        Routing.RegisterRoute("calendar_note_add", typeof(Calendar_note_add));
        Routing.RegisterRoute("calendar_note_edit", typeof(Calendar_note_edit));
        Routing.RegisterRoute("create_group", typeof(Create_group));
        Routing.RegisterRoute("groups", typeof(Groups));
#if WINDOWS
        SetTabBarIsVisible(TabBar_t, false);
#endif
    }
}
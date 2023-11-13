using Microsoft.Maui.Controls;

namespace SmartStudy.Views.Teacher;

public partial class AppShell_Teacher : Shell
{
    public AppShell_Teacher()
    {
        InitializeComponent();
        Routing.RegisterRoute("main_page", typeof(MainPage_Teacher));
        Routing.RegisterRoute("calendar", typeof(Calendar));
        Routing.RegisterRoute("groups", typeof(Groups));
#if WINDOWS
        SetTabBarIsVisible(TabBar_t, false);
#endif
    }
}
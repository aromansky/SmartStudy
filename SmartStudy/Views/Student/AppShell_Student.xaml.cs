using SmartStudy.Views.CalendarPages;
using SmartStudy.Views.GroupPages;
namespace SmartStudy.Views.Student;

public partial class AppShell_Student : Shell
{
	public AppShell_Student()
	{
		InitializeComponent();

        Routing.RegisterRoute("main_page", typeof(MainPage_Student));
        Routing.RegisterRoute("calendar", typeof(Calendar));
        Routing.RegisterRoute("calendar_note_edit", typeof(Calendar_note_edit));
        Routing.RegisterRoute("feedback", typeof(Feedback));
        Routing.RegisterRoute("homework", typeof(Homework));
        Routing.RegisterRoute("groups", typeof(Groups));
        Routing.RegisterRoute("edit_group", typeof(Edit_group));
#if WINDOWS
    SetTabBarIsVisible(TabBar_s, false);
#endif
    }
}
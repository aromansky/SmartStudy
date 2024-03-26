using SmartStudy.Views.CalendarPages;
using SmartStudy.Views.GroupPages;
using SmartStudy.Views.HomeworkPages;
namespace SmartStudy.Views.Student;

public partial class AppShell_Student : Shell
{
	public AppShell_Student()
	{
		InitializeComponent();
        Routing.UnRegisterRoute("main_page");
        Routing.UnRegisterRoute("calendar");
        Routing.UnRegisterRoute("feedback");
        Routing.UnRegisterRoute("calendar");
        Routing.UnRegisterRoute("homework");
        Routing.UnRegisterRoute("groups");
        Routing.UnRegisterRoute("edit_group");
        Routing.UnRegisterRoute("list_all_hw_user");
        Routing.UnRegisterRoute("list_all_hw_one_group");

        Routing.RegisterRoute("main_page", typeof(MainPage_Student));
        Routing.RegisterRoute("calendar", typeof(Calendar));
        Routing.RegisterRoute("calendar_note_edit", typeof(Calendar_note_edit));
        Routing.RegisterRoute("feedback", typeof(Feedback));
        Routing.RegisterRoute("homework", typeof(Homework));
        Routing.RegisterRoute("groups", typeof(Groups));
        Routing.RegisterRoute("edit_group", typeof(Edit_group));
        Routing.RegisterRoute("view_one_hw", typeof(View_one_hw));
        Routing.RegisterRoute("list_all_hw_user", typeof(List_all_hw_user));
        Routing.RegisterRoute("list_all_hw_one_group", typeof(List_all_hw_one_group));
#if WINDOWS
        SetTabBarIsVisible(TabBar_s, false);
#endif
    }
}
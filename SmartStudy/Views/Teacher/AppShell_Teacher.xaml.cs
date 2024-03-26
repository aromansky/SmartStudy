using SmartStudy.Views.CalendarPages;
using SmartStudy.Views.GroupPages;
using SmartStudy.Views.HomeworkPages;
namespace SmartStudy.Views.Teacher;

public partial class AppShell_Teacher : Shell
{
    public AppShell_Teacher()
    {
        InitializeComponent();
        Routing.UnRegisterRoute("main_page");
        Routing.UnRegisterRoute("calendar");
        Routing.UnRegisterRoute("feedback");
        Routing.UnRegisterRoute("calendar");
        Routing.UnRegisterRoute("homework");
        Routing.UnRegisterRoute("groups");
        Routing.UnRegisterRoute("edit_group");
        Routing.UnRegisterRoute("add_hw");
        Routing.UnRegisterRoute("view_one_hw");
        Routing.UnRegisterRoute("list_all_hw_user");
        Routing.UnRegisterRoute("list_all_group_with_hw");
        Routing.UnRegisterRoute("list_all_hw_one_group");

        Routing.RegisterRoute("main_page", typeof(MainPage_Teacher));
        Routing.RegisterRoute("calendar", typeof(Calendar));
        Routing.RegisterRoute("calendar_note_add", typeof(Calendar_note_add));
        Routing.RegisterRoute("calendar_note_edit", typeof(Calendar_note_edit));
        Routing.RegisterRoute("add_event_to_group", typeof(Group_list));
        Routing.RegisterRoute("groups", typeof(Groups));
        Routing.RegisterRoute("edit_group", typeof(Edit_group));
        Routing.RegisterRoute("create_group", typeof(Create_group));
        Routing.RegisterRoute("add_users_to_group", typeof(Users_list));
        Routing.RegisterRoute("feedback", typeof(Feedback));
        Routing.RegisterRoute("add_hw", typeof(Add_hw));
        Routing.RegisterRoute("view_one_hw", typeof(View_one_hw));
        Routing.RegisterRoute("list_all_hw_user", typeof(List_all_hw_user));
        Routing.RegisterRoute("list_all_groups_with_hw", typeof(List_all_groups_with_hw));
        Routing.RegisterRoute("list_all_hw_one_group", typeof(List_all_hw_one_group));
#if WINDOWS
        SetTabBarIsVisible(TabBar_t, false);
#endif
    }
}
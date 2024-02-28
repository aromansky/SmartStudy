using Plugin.Maui.Calendar.Models;
using SmartStudy.Models;
using SmartStudy.ModelsDB;
using System.Windows.Input;
namespace SmartStudy.Views.CalendarPages;

public partial class Calendar : ContentPage
{
    public EventCollection Events_1 { get; set; } = new EventCollection();
    public ICommand Getting_event_id_command { set; get; }
    public List<Event> temp_l = new List<Event>();

    public async void create_dict()
    {
        temp_l.Clear();
        Events_1.Clear();
        temp_l = await Client.GetEventsWithUser(Serializer.DeserializeUser());
        List<DateTime> t_date_list = new List<DateTime>();
        foreach (var item in temp_l)
        {
            List<Event> temp_col = new List<Event>();
            temp_col.Add(item);
            if (Events_1.ContainsKey(item.date_begin.Date))
                foreach (var item_old in Events_1[item.date_begin.Date])
                    temp_col.Add((Event)item_old);
            Events_1[item.date_begin.Date] = temp_col.OrderBy(p => p.date_begin).ToList();
        }
    }

    public Calendar()
	{
        InitializeComponent();
        BindingContext = new Calendar_note();

        Getting_event_id_command = new Command<long>(event_in_view_clicked);

        //create_dict();
        cal_view.Events = Events_1;
        cal_view.EventTemplate = new DataTemplate(() =>
        {
            Button event_button = new Button();
            event_button.FontSize = 22;
            event_button.SetBinding(Button.TextProperty, "Title");
            event_button.Command = Getting_event_id_command;
            event_button.SetBinding(Button.CommandParameterProperty, "event_id");
            Label lab_desk = new Label();
            lab_desk.SetBinding(Label.TextProperty, "Description");
            Label lab_time_begin = new Label();
            lab_time_begin.SetBinding(Label.TextProperty, "date_begin");
            lab_time_begin.HorizontalTextAlignment = TextAlignment.End;
            Label lab_time_end = new Label();
            lab_time_end.SetBinding(Label.TextProperty, "date_end");
            lab_time_end.HorizontalTextAlignment = TextAlignment.End;
            VerticalStackLayout vsl = new VerticalStackLayout
            {
                Padding = new Thickness(0, 5),
                Children = { event_button, lab_desk, lab_time_begin, lab_time_end }
            };
            return new Frame
            {
                Content = vsl
            };
        });

#if WINDOWS
#else
        row_button.Height = 0;
#endif
        }

    public async void update_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///main_page");
        await Shell.Current.GoToAsync("///calendar");
    }

    public async void clicked_to_main_page(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///main_page");
    }
    public async void clicked_to_groups(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///groups");
    }
    public async void clicked_to_feedback(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///feedback");
    }
    public async void clicked_to_homework(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///homework");
    }
    public async void clicked_event_add(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("calendar_note_add");
    }
    protected override void OnAppearing()
    {
        ((Calendar_note)BindingContext).Load_All_Events();
        create_dict();
        if (!Serializer.DeserializeUser().IsTutor())
        {
            Button_add_date.IsEnabled = false;
            Button_add_date.IconImageSource = null;


            AddEvent.IsEnabled = false;
            AddEvent.IsVisible = false;
        }
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("calendar_note_add");
    }

    private async void event_clicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            await Shell.Current.GoToAsync($"calendar_note_edit?note_id={((ModelsDB.Event)e.CurrentSelection[0]).event_id}");
            all_notes.SelectedItem = null;
        }
    }

    private void change_view_cal(object sender, EventArgs e)
    {
        if (grid_cal_view.IsVisible)
        {
            grid_cal_view.IsVisible = false;
            grid_cal_list.IsVisible = true;
            AddEvent.IsVisible = true;
        }
        else
        {
            grid_cal_view.IsVisible = true;
            grid_cal_list.IsVisible = false;
            AddEvent.IsVisible = false;
        }
    }

    public async void event_in_view_clicked(long event_id)
    {
        await Shell.Current.GoToAsync($"calendar_note_edit?note_id={event_id}");
    }

}
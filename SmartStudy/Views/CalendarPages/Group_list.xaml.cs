using SmartStudy.ModelsDB;

namespace SmartStudy.Views.CalendarPages;

[QueryProperty(nameof(Note_get_Id), "note_id")]
public partial class Group_list : ContentPage
{
    long event_id;
    Event @event;

    public long Note_get_Id
    {
        set { LoadNote_id(value); }
    }

    private async void LoadNote_id(long text_obj)
    {
        event_id = text_obj;
        @event = await Client.GetEventFromId(event_id);
    }
    public Group_list()
    {
        InitializeComponent();
        BindingContext = new Models.Group_note();
    }
    private async void ok_Clicked(object sender, EventArgs e)
    {
        if (((Models.Group_note)BindingContext).SelectedGroups.Count() == 0)
        {
            DisplayAlert("Ошибка", "Выберите не менее 1 группы", "Ok");
            return;
        }
        group_settings[] a = ((Models.Group_note)BindingContext).SelectedGroups.Select(x => x as group_settings).ToArray();
        Client.AddEventToGroup(@event, a);
        await Shell.Current.GoToAsync("///calendar");
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///calendar");
    }
}
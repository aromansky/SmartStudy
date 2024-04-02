using SmartStudy.ModelsDB;

namespace SmartStudy.Views.CalendarPages;

public partial class Group_list : ContentPage
{
    long event_id;
    Event @event;

    public Group_list(long event_id)
    {
        InitializeComponent();
        this.event_id = event_id;
        BindingContext = new Models.Group_note(); // Получаем группы, к которым не добавлено событие
    }
    private async void ok_Clicked(object sender, EventArgs e)
    {
        @event = await Client.GetEventFromId(event_id);

        if (((Models.Group_note)BindingContext).SelectedGroups.Count() == 0)
        {
            DisplayAlert("Ошибка", "Выберите не менее 1 группы", "Ok");
            return;
        }

        group_settings[] selectedGroups = ((Models.Group_note)BindingContext).SelectedGroups.Select(x => x as group_settings).ToArray();
        Client.AddEventToGroup(@event, selectedGroups);

        await Shell.Current.GoToAsync("///calendar");
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///calendar");
    }
}
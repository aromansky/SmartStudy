namespace SmartStudy.Models
{
    class Navigation_Windows
    {
        public async void clicked_to_main_page(object sender, EventArgs e)
        {
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
    }
}

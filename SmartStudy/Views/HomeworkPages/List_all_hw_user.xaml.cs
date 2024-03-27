using SmartStudy.Models;

namespace SmartStudy.Views.HomeworkPages;

public partial class List_all_hw_user : ContentPage
{
	public List_all_hw_user()
	{
		InitializeComponent();
        long user_id = Serializer.DeserializeUser().user_id;
        if (Serializer.DeserializeUser().IsTutor())
		    BindingContext = Client.GetHomeworkForAuthor(user_id);
        else
            BindingContext = Client.GetUserHomework(user_id);
    }

	public async void homework_ckicked(object sender, SelectionChangedEventArgs e)
	{
        if (e.CurrentSelection.Count != 0)
        {
            await Shell.Current.GoToAsync($"view_one_hw?hw_id={((ModelsDB.homework)e.CurrentSelection[0]).homework_id}");
            all_homeworks_user.SelectedItem = null;
        }
    }
}
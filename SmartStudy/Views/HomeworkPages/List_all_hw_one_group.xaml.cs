namespace SmartStudy.Views.HomeworkPages;

[QueryProperty(nameof(Group_get_Id), "group_id")]
public partial class List_all_hw_one_group : ContentPage
{
    private long group_id;
    public long Group_get_Id
    {
        set { group_id = value; }
    }
    public List_all_hw_one_group()
	{
		InitializeComponent();
        BindingContext = BindingContext = Client.GetGroupHomework(group_id);

    }
    public async void homework_ckicked(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            await Shell.Current.GoToAsync($"view_one_hw?hw_id={((ModelsDB.homework)e.CurrentSelection[0]).homework_id}");
            all_homeworks_group.SelectedItem = null;
        }
    }
}
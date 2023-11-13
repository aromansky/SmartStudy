using SmartStudy.Models;
using System.Windows.Input;

namespace SmartStudy.Views;

public partial class RegistrationPage : ContentPage
{
	Models.Registration registration = new Models.Registration();
    public ICommand TapCommand => new Command<string>(async (url) => await Shell.Current.GoToAsync(url));
    private void resize_el()
    {
#if WINDOWS
        Layout_registration.VerticalOptions = LayoutOptions.Center;
        Screen_Size screen_size = new Screen_Size();
        Layout_registration.WidthRequest = screen_size.Width/6;
#endif
    }
    public RegistrationPage()
	{
		InitializeComponent();
		resize_el();
		BindingContext = this;
	}
	private void Load_registration()
	{
		registration.first_name = entry_first_name.Text;
		registration.last_name = entry_last_name.Text;
		registration.Email = entry_Email.Text;
		registration.password = entry_password.Text;
		registration.password_2 = entry_password_2.Text;
	}
	private async void wind_error(string text_error)
	{
        await DisplayAlert("", text_error, "ОK");
    }
	public async void button_registration_clicked(object sender, EventArgs e)
    {
        Load_registration();
		if ((registration.last_name == null) | (registration.first_name == null) |  (registration.Email == null)
			| (registration.password == null) | (registration.password_2 == null) | (registration.last_name == "")
			| (registration.first_name == "") |  (registration.Email == "") | (registration.password == "")
            | (registration.password_2 == ""))
		{
			wind_error("Заполнены не все поля!");

		}
		else if (registration.Email == "уже есть") 
		{
			//проверить базу данных на наличие указанной почты
            wind_error("Данная почта уже зарегистрирована!");
        }
		else if (registration.password != registration.password_2)
		{
            wind_error("Введённые пароли не совпадают!");
        }
		else
		{
            //проверить нет ли таких же данных в базе
            //если нет, то добавить и
			if (registration.Email == "student")
			{
                Application.Current.MainPage = new Student.AppShell_Student();
                await Shell.Current.GoToAsync("///main_page");
            }
			else if (registration.Email == "teacher")
			{
				Application.Current.MainPage = new Teacher.AppShell_Teacher();
				await Shell.Current.GoToAsync("///main_page");
			}
            
        }
    }
}
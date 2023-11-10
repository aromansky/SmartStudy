using SmartStudy.Models;
using System.Windows.Input;

namespace SmartStudy.Views;

public partial class LoginPage : ContentPage
{
    Login login = new Login();
    public ICommand TapCommand => new Command<string>(async (url) => await Shell.Current.GoToAsync(url));
    private void resize_el() 
    {
#if WINDOWS
        Layout_Login.VerticalOptions = LayoutOptions.Center;
        Screen_Size screen_size = new Screen_Size();
        Layout_Login.WidthRequest = screen_size.Width/6;
#endif
    }
    public LoginPage()
	{
		InitializeComponent();
        resize_el();
        BindingContext = this;
    }
    private void Load_login()
    {
        login.Email = entry_Email.Text;
        login.password = entry_password.Text;
    }
    private async void wind_error(string text_error)
    {
        await DisplayAlert("", text_error, "ОK");
    }
    public async void button_login_clicked(object sender, EventArgs e)
	{
        Load_login();
        if ((login.Email == "") | (login.password == "") | (login.Email == null) | (login.password == null)) 
        {
            wind_error("Заполнены не все поля!");
        }
        else if(false)
        {
            //проверить по базе данных наличие почты
            wind_error("Аккаунта, зарегистрированного на данную почту не существует!");
        }
        else if(login.password != "1") 
        {
            wind_error("Неверный пароль!");
        }
		else if(login.Email == "student")
        {
            Application.Current.MainPage = new Student.AppShell_Student();
            await Shell.Current.GoToAsync("///main_page");
        }
        else if (login.Email == "teacher")
        {
            Application.Current.MainPage = new Teacher.AppShell_Teacher();
            await Shell.Current.GoToAsync("///main_page");
        }
    }
}
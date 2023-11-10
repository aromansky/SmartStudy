namespace SmartStudy
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute("login", typeof(Views.LoginPage));
            Routing.RegisterRoute("registration", typeof(Views.RegistrationPage));
        }
    }
}
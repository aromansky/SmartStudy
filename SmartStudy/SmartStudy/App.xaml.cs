namespace SmartStudy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            UserAppTheme = AppTheme.Dark;
            MainPage = new AppShell();
            
        }
        //private displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        
    }
}
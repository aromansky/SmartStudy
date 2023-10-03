namespace Prototype1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        void OnClicked1(object sender, EventArgs e)
        {
            Biliboba.Text = Button1.Text;
        }

        void OnClicked2(object sender, EventArgs e)
        {
            Biliboba.Text = Button2.Text;
        }

        void OnClicked3(object sender, EventArgs e)
        {
            Biliboba.Text = Button3.Text;
        }

        void OnClicked4(object sender, EventArgs e)
        {
            Biliboba.Text = Button4.Text;
        }
    }
}
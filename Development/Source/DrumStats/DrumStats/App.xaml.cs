using DrumStats.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DrumStats
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new NewGamePage())
                    {
                        Title = "Game",
                        Icon = Device.OnPlatform<string>("tab_feed.png",null,null),
                    },
                    new NavigationPage(new StatsPage())
                    {
                        Title = "Stats",
                        Icon = Device.OnPlatform<string>("tab_about.png",null,null)
                    },
                    new NavigationPage(new PlayersPage())
                    {
                        Title = "Players",
                        Icon = Device.OnPlatform<string>("tab_about.png",null,null)
                    },
                    new NavigationPage(new SettingsPage())
                    {
                        Title = "Settings",
                        Icon = Device.OnPlatform<string>("tab_about.png",null,null)
                    },
                },
            };
        }
    }
}

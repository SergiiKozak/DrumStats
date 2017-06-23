using DrumStats.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrumStats.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayersPage : ContentPage
	{
        PlayersViewModel viewModel;
		public PlayersPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new PlayersViewModel();
            SetUpToolbar();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Players.Count == 0)
                viewModel.LoadPlayersCommand.Execute(null);
        }

        private void SetUpToolbar()
        {
            var addPlayerItem = new ToolbarItem()
            {
                Text = "Add Player",
                Order = ToolbarItemOrder.Primary
            };

            addPlayerItem.Clicked += OnAddPlayer_Clicked;

            switch(Device.RuntimePlatform)
            {
                case Device.Windows:
                    addPlayerItem.Icon = "additem-64.png";
                    break;
            }

            ToolbarItems.Add(addPlayerItem);

        }

        async void OnAddPlayer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewPlayerPage());
        }
    }
}
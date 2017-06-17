using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrumStats.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DrumStats.Models;
using DrumStats.Controls;

namespace DrumStats.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewGamePage : ContentPage
	{
        NewGameViewModel viewModel;
		public NewGamePage()
		{
			InitializeComponent ();
            BindingContext = viewModel = new NewGameViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Players.Count == 0)
                viewModel.LoadPlayersCommand.Execute(null);
        }

        void OnPlayerSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var player = args.SelectedItem as Player;
            if (player == null)
                return;

            viewModel.SelectPlayer(player);

            // Manually deselect item
            PlayersListView1.SelectedItem = null;
            PlayersListView2.SelectedItem = null;
        }

        void OnBlueScoreButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var score = int.Parse(button.Text);
            viewModel.SelectScore(score, TeamColor.Blue);
        }

        void OnRedScoreButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var score = int.Parse(button.Text);
            viewModel.SelectScore(score, TeamColor.Red);
        }

        void OnClearButtonClicked(object sender, EventArgs e)
        {
            viewModel.ResetGame();
        }

        async void OnSaveAndNextButtonClicked(object sender, EventArgs e)
        {
            await viewModel.SaveAndNext();
        }
    }
}
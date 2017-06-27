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
            var result = await viewModel.SaveAndNext();

            if (result)
            {
                var nextGameViewModel = new NewGameViewModel();
                await Task.Run(() => nextGameViewModel.LoadPlayersCommand.Execute(null));

                nextGameViewModel.Initialize(viewModel.Game.Clone(), viewModel.ConsequentWins);

                MessagingCenter.Send(this, "GameSaved", viewModel.Game.Clone());

                BindingContext = viewModel = nextGameViewModel;

                OnBindingContextChanged();
            }
        }
    }
}
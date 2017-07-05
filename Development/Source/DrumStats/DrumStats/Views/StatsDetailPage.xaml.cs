using DrumStats.Models.Statistics;
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
	public partial class StatsDetailPage : ContentPage
	{
        private StatsDetailViewModel viewModel;
		public StatsDetailPage(PlayerStats playerStats)
		{
			InitializeComponent();
            BindingContext = viewModel = new StatsDetailViewModel(playerStats);
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Players.Count == 0)
                viewModel.LoadPlayersCommand.Execute(null);
        }
    }
}
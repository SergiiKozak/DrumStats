using DrumStats.Helpers;
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
    public partial class StatsPage : ContentPage
    {
        private StatsViewModel viewModel;
        public StatsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new StatsViewModel();
            SetUpToolbar();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.PlayerStats.Count == 0)
                viewModel.LoadDataCommand.Execute(null);
        }

        private void SetUpToolbar()
        {
            var refreshItem = new ToolbarItem()
            {
                Text = "Refresh",
                Order = ToolbarItemOrder.Primary
            };

            refreshItem.Clicked += OnRefresh_Clicked;

            switch (Device.RuntimePlatform)
            {
                case Device.Windows:
                    refreshItem.Icon = "synchronize-64.png";
                    break;
            }

            ToolbarItems.Add(refreshItem);

        }

        void OnRefresh_Clicked(object sender, EventArgs e)
        {
            viewModel.LoadDataCommand.Execute(null);
        }

        private void OnWinRatesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var item = (Pair<PlayerStats, PlayerStats>)e.SelectedItem;
                Navigation.PushAsync(new StatsDetailPage(item.First));
                WinRatesListView.SelectedItem = null;
            }
        }
    }
}
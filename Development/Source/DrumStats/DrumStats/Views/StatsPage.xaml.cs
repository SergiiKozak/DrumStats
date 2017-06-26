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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.PlayerStats.Count == 0)
                viewModel.LoadDataCommand.Execute(null);
        }
    }
}
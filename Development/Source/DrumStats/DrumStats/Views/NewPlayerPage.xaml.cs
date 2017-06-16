using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DrumStats.ViewModels;

namespace DrumStats.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewPlayerPage : ContentPage
	{
        NewPlayerViewModel viewModel;
		public NewPlayerPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new NewPlayerViewModel();
		}

        async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddPlayer", viewModel.Player);
            await Navigation.PopToRootAsync();
        }

        async void OnCancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
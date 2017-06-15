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

        public async Task OnSaveButton_Clicked()
        {
            var res = await viewModel.SavePlayer();

            if (res)
                await Navigation.PopAsync();
        }

        public async Task OnCancelButton_Clicked()
        {
            await Navigation.PopAsync();
        }
    }
}
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using DrumStats.Helpers;
using DrumStats.Models;
using DrumStats.Views;
using Xamarin.Forms;

namespace DrumStats.ViewModels
{
    class NewPlayerViewModel : BaseViewModel
    {
        public Player Player { get; set; }

        public NewPlayerViewModel()
        {
            Player = new Player();
        }
    }
}

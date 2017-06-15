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

        public Command SavePlayerCommand { get; set; }

        public NewPlayerViewModel()
        {
            Player = new Player();
            SavePlayerCommand = new Command(async () => await SavePlayer());
        }

        public async Task<bool> SavePlayer()
        {
            var res = await PlayerDataStore.AddItemAsync(Player);

            return res;
        }
    }
}

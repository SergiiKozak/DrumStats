using DrumStats.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrumStats.Models.Statistics
{
    public class PlayerStats : ObservableObject
    {
        private Player player;
        public Player Player
        {
            get { return player; }
            set { SetProperty(ref player, value); }
        }

        private WinRate winRate;
        public WinRate WinRate
        {
            get { return winRate; }
            set { SetProperty(ref winRate, value); }
        }

        public PlayerStats()
        {

        }

        public PlayerStats(Player player, WinRate winRate)
        {
            Player = player;
            WinRate = winRate;
        }
    }
}

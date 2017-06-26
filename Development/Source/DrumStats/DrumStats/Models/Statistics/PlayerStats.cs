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

        private WinRate winRateAbsolute;
        public WinRate WinRateAbsolute
        {
            get { return winRateAbsolute; }
            set { SetProperty(ref winRateAbsolute, value); }
        }

        private WinRate winRateRelative;
        public WinRate WinRateRelative
        {
            get { return winRateRelative; }
            set { SetProperty(ref winRateRelative, value); }
        }

        private PlayCount playCount;
        public PlayCount PlayCount
        {
            get { return playCount; }
            set { SetProperty(ref playCount, value); }
        }

        public PlayerStats()
        {

        }
    }
}

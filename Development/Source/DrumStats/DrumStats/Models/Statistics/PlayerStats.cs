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

        private Rate winRateAbsolute;
        public Rate WinRateAbsolute
        {
            get { return winRateAbsolute; }
            set { SetProperty(ref winRateAbsolute, value); }
        }

        private Rate winRateRelative;
        public Rate WinRateRelative
        {
            get { return winRateRelative; }
            set { SetProperty(ref winRateRelative, value); }
        }

        private Rate goalRate;
        public Rate GoalRate
        {
            get { return goalRate; }
            set { SetProperty(ref goalRate, value); }
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

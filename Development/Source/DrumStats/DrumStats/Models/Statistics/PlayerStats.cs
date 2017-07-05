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

        private Person bestPartner;
        public Person BestPartner
        {
            get { return bestPartner; }
            set { SetProperty(ref bestPartner, value); }
        }

        private Person worstPartner;
        public Person WorstPartner
        {
            get { return worstPartner; }
            set { SetProperty(ref worstPartner, value); }
        }

        private Person victim;
        public Person Victim
        {
            get { return victim; }
            set { SetProperty(ref victim, value); }
        }

        private Person nemesis;
        public Person Nemesis
        {
            get { return nemesis; }
            set { SetProperty(ref nemesis, value); }
        }

        public PlayerStats()
        {

        }
    }
}

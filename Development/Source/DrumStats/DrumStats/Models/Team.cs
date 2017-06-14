using DrumStats.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrumStats.Models
{
    public class Team : ObservableObject
    {
        private Player attack;
        public Player Attack
        {
            get { return attack; }
            set { SetProperty(ref attack, value); }
        }

        private Player defence;
        public Player Defence
        {
            get { return defence; }
            set { SetProperty(ref defence, value); }
        }

        private int score;
        public int Score
        {
            get { return score; }
            set { SetProperty(ref score, value); }
        }
    }
}

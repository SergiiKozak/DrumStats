using DrumStats.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace DrumStats.Models
{
    public class Team : ObservableObject
    {
        private Player attack;
        [JsonProperty("offense")]
        public Player Attack
        {
            get { return attack; }
            set { SetProperty(ref attack, value); }
        }

        private Player defence;
        [JsonProperty("defense")]
        public Player Defence
        {
            get { return defence; }
            set { SetProperty(ref defence, value); }
        }

        private int score;
        [JsonProperty("score")]
        public int Score
        {
            get { return score; }
            set { SetProperty(ref score, value); }
        }
    }
}

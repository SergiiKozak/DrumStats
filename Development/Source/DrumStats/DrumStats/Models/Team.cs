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

        [JsonIgnore]
        public TeamColor TeamColor { get; set; }

        public Team()
            : this(TeamColor.Undefined)
        {

        }

        public Team(TeamColor teamColor)
        {
            TeamColor = teamColor;
        }

        public void Reset()
        {
            Attack = null;
            Defence = null;
            Score = 0;
        }

        public Team Clone()
        {
            var clone = new Team(TeamColor)
            {
                Attack = Attack.Clone(),
                Defence = Defence.Clone(),
                Score = Score
            };

            return clone;
        }
    }
}

using DrumStats.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrumStats.Models.Statistics
{
    public class Person : ObservableObject
    {
        private string playerId;
        [JsonProperty("Id")]
        public string PlayerId
        {
            get { return playerId; }
            set { SetProperty(ref playerId, value); }
        }

        private string attackId;
        [JsonProperty("offenseId")]
        public string AttackId
        {
            get { return attackId; }
            set { SetProperty(ref attackId, value); }
        }

        private string defenceId;
        [JsonProperty("defenseId")]
        public string DefenceId
        {
            get { return defenceId; }
            set { SetProperty(ref defenceId, value); }
        }

        private string totalId;
        [JsonProperty("totalId")]
        public string TotalId
        {
            get { return totalId; }
            set { SetProperty(ref totalId, value); }
        }
    }
}

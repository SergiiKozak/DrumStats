using System;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;
using DrumStats.Helpers;

namespace DrumStats.Models
{
    public class Game : BaseDataObject
    {
        private DateTime? startDate;
        [JsonProperty("startDate")]
        public DateTime? StartDate
        {
            get { return startDate; }
            set { SetProperty(ref startDate, value); }
        }

        private DateTime? endDate;
        [JsonProperty("endDate")]
        public DateTime? EndDate
        {
            get { return endDate; }
            set { SetProperty(ref endDate, value); }
        }

        private string source;
        [JsonProperty("source")]
        public string Source
        {
            get { return source; }
            set { SetProperty(ref source, value); }
        }

        private object metadata;
        [JsonProperty("metadata")]
        public object MetaData
        {
            get { return metadata; }
            set { SetProperty(ref metadata, value); }
        }

        private Team blueTeam;
        [JsonProperty("blue")]
        public Team BlueTeam
        {
            get { return blueTeam; }
            set { SetProperty(ref blueTeam, value); }
        }

        private Team redTeam;
        [JsonProperty("red")]
        public Team RedTeam
        {
            get { return redTeam; }
            set { SetProperty(ref redTeam, value); }
        }

        [JsonIgnore]
        public Team Winner
        {
            get
            {
                if (BlueTeam.Score == RedTeam.Score)
                    return null;

                return BlueTeam.Score > RedTeam.Score ? BlueTeam : RedTeam;
            }
        }

        [JsonIgnore]
        public Team Loser
        {
            get
            {
                if (BlueTeam.Score == RedTeam.Score)
                    return null;

                return BlueTeam.Score > RedTeam.Score ? RedTeam : BlueTeam;
            }
        }

        public Team GetTeam(TeamColor teamColor)
        {
            switch (teamColor)
            {
                case TeamColor.Blue:
                    return BlueTeam;
                case TeamColor.Red:
                    return RedTeam;
                default:
                    return null;
            }
        }

        public Game()
        {
            blueTeam = new Team(TeamColor.Blue);
            redTeam = new Team(TeamColor.Red);

            Source = string.Format("DrumStats/{0}", Device.RuntimePlatform == Device.UWP ? "Windows" : Device.RuntimePlatform);
        }

        public void Reset()
        {
            Id = string.Empty;
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
            metadata = null;
            blueTeam.Reset();
            redTeam.Reset();
        }

        public Game Clone()
        {
            var clone = new Game()
            {
                Id = Id,
                StartDate = StartDate,
                EndDate = EndDate,
                Source = Source,
                MetaData = MetaData,
                BlueTeam = BlueTeam.Clone(),
                RedTeam = RedTeam.Clone()
            };

            return clone;
        }
    }
}

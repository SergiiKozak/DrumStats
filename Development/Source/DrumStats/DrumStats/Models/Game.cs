using System;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;
using DrumStats.Helpers;

namespace DrumStats.Models
{
    public class Game : BaseDataObject
    {
        [JsonIgnore]
        public override string Id
        {
            get => base.Id;
            set => base.Id = value;
        }


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

        public Game()
        {
            blueTeam = new Team();
            redTeam = new Team();

            Source = string.Format("DrumStats/{0}", Device.RuntimePlatform);
        }

        public void Reset()
        {
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
            metadata = null;
            blueTeam.Reset();
            redTeam.Reset();
        }
    }
}

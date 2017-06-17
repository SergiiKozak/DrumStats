using System;
using DrumStats.Helpers;
using Newtonsoft.Json;

namespace DrumStats.Models
{
    public class BaseDataObject : ObservableObject
    {
        string id = string.Empty;
        [JsonIgnore]
        public string Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        [JsonProperty(PropertyName = "_id")]
        protected string DeserializedId
        {
            set
            {
                Id = value;
            }
        }

    }
}

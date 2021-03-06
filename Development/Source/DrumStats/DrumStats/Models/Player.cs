﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using DrumStats.Helpers;

namespace DrumStats.Models
{
    public class Player : BaseDataObject
    {
        string name = string.Empty;
        [JsonProperty(PropertyName = "firstName")]
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        string surname = string.Empty;
        [JsonProperty(PropertyName = "lastName")]
        public string Surname
        {
            get { return surname; }
            set { SetProperty(ref surname, value); }
        }

        [JsonIgnore]
        public string ShortDisplayName
        {
            get
            {
                return string.Format("{0} {1}", Name == "Volodymyr" ? "Vova" : Name, string.IsNullOrEmpty(Surname) ? string.Empty : Surname.Substring(0, 1));
            }
        }

        [JsonIgnore]
        public string DisplayName
        {
            get
            {
                return string.Format("{0} {1}", Name, Surname);
            }
        }

        public Player Clone()
        {
            var clone = new Player()
            {
                Id = Id,
                Name = Name,
                Surname = Surname
            };

            return clone;
        }
    }
}

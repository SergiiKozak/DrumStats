using System;
using System.Collections.Generic;
using System.Text;

namespace DrumStats.Models
{
    public class Game : BaseDataObject
    {
        private Team blueTeam;
        public Team BlueTeam
        {
            get { return blueTeam; }
            set { SetProperty(ref blueTeam, value); }
        }

        private Team redTeam;
        public Team RedTeam
        {
            get { return redTeam; }
            set { SetProperty(ref redTeam, value); }
        }

        public Game()
        {
            blueTeam = new Team();
            redTeam = new Team();
        }
    }
}

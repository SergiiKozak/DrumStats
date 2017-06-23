using DrumStats.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(DrumStats.Services.PlayerDataStore))]
namespace DrumStats.Services
{
    public class PlayerDataStore : RestDataStore<Player>
    {
        public override string ServerUri => "http://foosball-results.herokuapp.com/api/players";
    }
}

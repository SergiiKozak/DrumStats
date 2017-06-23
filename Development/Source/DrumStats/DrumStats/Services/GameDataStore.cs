using DrumStats.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(DrumStats.Services.GameDataStore))]
namespace DrumStats.Services
{
    public class GameDataStore : RestDataStore<Game>
    {
        public override string ServerUri => "http://foosball-results.herokuapp.com/api/games";
    }
}

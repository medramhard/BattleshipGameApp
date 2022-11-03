using System.Collections.Generic;

namespace BattleShipLibrary.Models
{
    public class PlayerModel
    {
        public string UsersName { get; set; }
        public List<GridSpotModel> ShipsLocations { get; set; } = new List<GridSpotModel>();
        public List<GridSpotModel> ShotGrid { get; set; } = new List<GridSpotModel>();
        public int ShotCount { get; internal set; }
    }
}

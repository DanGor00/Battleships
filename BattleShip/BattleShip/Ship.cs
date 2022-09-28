using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class Ship
    {
        public ShipType ShipType { get; set; }
        public List<(int, int)> ShipCoordinates { get; set; }

        public List<(int, int)> ShipCoordinatesStartPosition { get; set; }

        public void DeleteCoordinateFromShip(int rowCoordinate, int colCoordinate)
        {

            for (int i = 0; i < ShipCoordinates.Count; i++)
            {
                if (ShipCoordinates[i].Item1 == rowCoordinate && ShipCoordinates[i].Item2 == colCoordinate)
                {
                    ShipCoordinates.RemoveAt(i);
                    return;
                }
            }
        }
        public Ship(ShipType shipType)
        {
            this.ShipType = shipType;
            ShipCoordinates = new List<(int, int)>();
        }
    }
}

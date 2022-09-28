using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class Player
    {
        public int Score { get; set; }
        public int Name { get; set; }
        //public void SquareStatus(int rowCoordinate, int heightCoordinate)
        //{
        //    Board board = new Board();

        //}
        public List<Ship> Ships { get; set; }

        public bool WasShotBefore(Board boardToShoot, (int,int) coordinates)
        {
            int rowCoordinate = coordinates.Item1;
            int heightCoordinate = coordinates.Item2;
            if (boardToShoot.board[rowCoordinate, heightCoordinate].SquareStatus == SquareStatus.Missed 
            || boardToShoot.board[rowCoordinate, heightCoordinate].SquareStatus == SquareStatus.Hit
            || boardToShoot.board[rowCoordinate, heightCoordinate].SquareStatus == SquareStatus.Sunk)
            {
                Display display = new Display();
                display.PrintMsg("You already shoot at that target. Try again.");
                return true;
            }
            return false;
        }

        public bool ShootMechanic(Board board, (int, int) coordinates, Player player, Player player2)
        {
            if (board.board[coordinates.Item1, coordinates.Item2].SquareStatus == SquareStatus.Empty)
            {
                board.board[coordinates.Item1, coordinates.Item2].SquareStatus = SquareStatus.Missed;
                return false;
            }
            else if (board.board[coordinates.Item1, coordinates.Item2].SquareStatus == SquareStatus.Ship)
            {
                board.board[coordinates.Item1, coordinates.Item2].SquareStatus = SquareStatus.Hit;
                Game game = new Game();
                foreach (Ship ship in player2.Ships)
                {
                    
                    ship.DeleteCoordinateFromShip(coordinates.Item1, coordinates.Item2);
                    
                    if (ship.ShipCoordinates.Count == 0)
                    {
                        foreach ((int, int) coords in ship.ShipCoordinatesStartPosition)
                        {
                            board.board[coords.Item1, coords.Item2].SquareStatus = SquareStatus.Sunk;
                        }
                        player2.Ships.Remove(ship);
                        return true;
                    }
                }
                    
                return true;
            }
            return false;
        }

        public bool IsAlive(Player player)
        {
            if (player.Ships.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}

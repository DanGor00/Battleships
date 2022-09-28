using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class Square
    {
        public (int, int) Position { get; }
        public SquareStatus SquareStatus { get; set; }
        public Square((int,int) position)
        {
            SquareStatus = SquareStatus.Empty;
            this.Position = position;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class Board
    {
        public int Size = 10;
        public Square[,] board { get; set; }

        public int Owner { get; set; }

        public void CreatePlayerBoard()
        {
            board = new Square[Size,Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    board[i, j] = new Square((i, j));
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class BoardFactory
    {
        Random rnd = new Random();

        public List<(int, int)> RandomPlacement(int size, int shipSize, Board board)
        {
            Input input = new Input();
            Display display = new Display();
            int placement = -1;
            step1:
            int row = rnd.Next(size - 1);
            int col = rnd.Next(size - 1);

            List<int> rotationOptions = new List<int>();
            List<(int, int)> shipAllCoordinates = new List<(int, int)>();

            if (shipSize == 1) // first step does not require checking if warship in square status.
            {
                shipAllCoordinates.Add((row, col));
                return shipAllCoordinates;
            }
            // else if ship is bigger than 1
            if (board.board[row, col].SquareStatus != SquareStatus.Empty)
            {
                goto step1;
            }
            else
            {
                if (row + 1 <= shipSize)
                {
                    if (board.board[row + 1, col].SquareStatus != SquareStatus.Empty)
                        goto step1;
                }
                else if (row - 1 >= 0)
                {
                    if (board.board[row - 1, col].SquareStatus != SquareStatus.Empty)
                        goto step1;
                }
                else if (col + 1 <= shipSize)
                {
                    if (board.board[row + 1, col].SquareStatus != SquareStatus.Empty)
                        goto step1;
                }
                else if (row - 1 >= 0)
                {
                    if (board.board[row - 1, col].SquareStatus != SquareStatus.Empty)
                        goto step1;
                }


            }
            
            bool down = input.IsValidCoordinatesInput(row + shipSize - 1, col);
            if (down)
            {
                down = ShipCollision(row, col, shipSize, board, 1);
                if (down) rotationOptions.Add(4);
            }
            bool right = input.IsValidCoordinatesInput(row, col + shipSize - 1);
            if (right)
            {
                right = ShipCollision(row, col, shipSize, board, 2);
                if (right) rotationOptions.Add(1);
            }
            bool up = input.IsValidCoordinatesInput(row - shipSize + 1, col);
            if (up)
            {
                up = ShipCollision(row, col, shipSize, board, 4);
                if (up) rotationOptions.Add(2);
            }
            bool left = input.IsValidCoordinatesInput(row, col - shipSize + 1);
            if (left)
            {
                left = ShipCollision(row, col, shipSize, board, 3);
                if (left) rotationOptions.Add(3);
            }

            int listLen = rotationOptions.Count; //Checks if there are rotation options for ship
            if (listLen == 0) goto step1; // if there are no options, loop function
            //step2:
            //int x = rnd.Next(4);

            //if (rotationOptions.Contains(x))
            //{
            //    placement = x;
            //}
            //else
            //{
            //    display.BoardDisplay(board, 1);
            //    goto step2;
            //}

            placement = rotationOptions[0];
            shipAllCoordinates = GetAllShipCoordinates(row, col, placement, shipSize);

            return shipAllCoordinates;
        }

        public (int, int) ManualPlacement(int shipSize, Board board)
        {
            Input input = new Input();
            Display display = new Display();
            step1:
            (int row, int col) = input.PlayerCoordinatesInputConvert();
            if (shipSize == 1)
            {
                return (row, col);
            }
            else
            {
                if (board.board[row, col].SquareStatus != SquareStatus.Empty)
                {
                    Console.Clear();
                    display.PrintMsg("Warship collision");
                    goto step1;
                }
                bool down = input.IsValidCoordinatesInput(row + shipSize - 1, col);
                if (down)
                {
                    down = ShipCollision(row, col, shipSize, board, 1);
                }
                bool right = input.IsValidCoordinatesInput(row, col + shipSize - 1);
                if (right)
                {
                    down = ShipCollision(row, col, shipSize, board, 2);
                }
                bool up = input.IsValidCoordinatesInput(row - shipSize - 1, col);
                if (up)
                {
                    up = ShipCollision(row, col, shipSize, board, 4);
                }
                bool left = input.IsValidCoordinatesInput(row, col - shipSize - 1);
                if (left)
                {
                    left = ShipCollision(row, col, shipSize, board, 3);
                }
                int placement = input.playerPlacementOptions(right, down, left, up);
            }
            return (row, col);
        }

        public List<(int, int)> MyManualPlacement(int shipSize, Board board)
        {
            int placement = -1;
            List<(int, int)> shipAllCoordinates = new List<(int, int)> ();
            Input input = new Input();
            Display display = new Display();
            step1:
            (int row, int col) = input.PlayerCoordinatesInputConvert();
            if (shipSize == 1)
            {
                shipAllCoordinates.Add((row, col));
                return shipAllCoordinates;
            }
            else
            {
                if (board.board[row, col].SquareStatus != SquareStatus.Empty)
                {
                    Console.Clear();
                    display.PrintMsg("Warship collision");
                    goto step1;
                }
                bool down = input.IsValidCoordinatesInput(row + shipSize - 1, col);
                if (down)
                {
                    down = ShipCollision(row, col, shipSize, board, 1);
                }
                bool right = input.IsValidCoordinatesInput(row, col + shipSize - 1);
                if (right)
                {
                    right = ShipCollision(row, col, shipSize, board, 2);
                }
                bool up = input.IsValidCoordinatesInput(row - shipSize + 1, col);
                if (up)
                {
                    up = ShipCollision(row, col, shipSize, board, 4);
                }
                bool left = input.IsValidCoordinatesInput(row, col - shipSize + 1);
                if (left)
                {
                    left = ShipCollision(row, col, shipSize, board, 3);
                }
                placement = input.playerPlacementOptions(right, down, left, up);
            }

            shipAllCoordinates = GetAllShipCoordinates(row, col, placement, shipSize);
            return shipAllCoordinates;
        }

        private List<(int, int)> GetAllShipCoordinates(int row, int col, int placement, int shipSize)
        {
            List<(int, int)> shipAllCoordinates = new List<(int, int)>();
            shipAllCoordinates.Add((row, col));
            switch (placement)
            {
                case 1:
                    for (int i = 1; i < shipSize; i++)
                    {
                        col++;
                        shipAllCoordinates.Add((row, col));
                    }
                    break;
                case 2:
                    for (int i = 1; i < shipSize; i++)
                    {
                        row--;
                        shipAllCoordinates.Add((row, col));
                    }
                    break;
                case 3:
                    for (int i = 1; i < shipSize; i++)
                    {
                        col--;
                        shipAllCoordinates.Add((row, col));
                    }
                    break;
                case 4:
                    for (int i = 1; i < shipSize; i++)
                    {
                        row++;
                        shipAllCoordinates.Add((row, col));
                    }
                    break;
                case -1:
                    break;
            }


            return shipAllCoordinates;
        }

        private void UpdateAllSquaresToShip()
        {

        }

        private bool ShipCollision(int row, int col, int shipSize, Board board, int variation)
        {
            switch (variation)
            {
                case 1:
                    try
                    {
                        for (int i = 0; i < shipSize; i++)
                        {
                            if (board.board[row + i, col].SquareStatus != SquareStatus.Empty)
                            {
                                return false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                    if (row + shipSize <= board.Size - 1)
                    {
                        if (board.board[row + shipSize, col].SquareStatus != SquareStatus.Empty)
                        {
                            return false;
                        }

                    }
                    return true;
                    break;
                case 2:
                    try
                    {
                        for (int i = 0; i < shipSize; i++)
                        {
                            if (board.board[row, col + i].SquareStatus != SquareStatus.Empty)
                            {
                                return false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                    if (col + shipSize <= board.Size - 1)
                    {
                        if (board.board[row, col + shipSize].SquareStatus != SquareStatus.Empty)
                        {
                            return false;
                        }

                    }
                    return true;
                    break;
                case 3:
                    try
                    {
                        for (int i = 0; i < shipSize; i++)
                        {
                            if (board.board[row, col - i].SquareStatus != SquareStatus.Empty)
                            {
                                return false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                    if (col - shipSize >= 0)
                    {
                        if (board.board[row, col - shipSize].SquareStatus != SquareStatus.Empty)
                        {
                            return false;
                        }

                    }
                    return true;
                    break;
                case 4:
                    try
                    {
                        for (int i = 0; i < shipSize; i++)
                        {
                            if (board.board[row - i, col].SquareStatus != SquareStatus.Empty)
                            {
                                return false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                    if (row - shipSize >= 0)
                    {
                        if (board.board[row - shipSize, col].SquareStatus != SquareStatus.Empty)
                        {
                            return false;
                        }

                    }
                    return true;
                    break;
                    
            }

            return false;
        }
    }
}

using System;

namespace BattleShip
{
    internal class Display
    {
        public static ConsoleColor ForegroundColor { get; set; }
        public void DisplayMainMenu()
        {
            string mainMenu = "   BATTLESHIP\n\n1. Player vs Player\n2. Player vs Computer\n3. Options\n4. Scores\n0. Exit";
            Console.WriteLine(mainMenu);
        }

        public void DisplayProvideInput(string inputType)
        {
            string information = $"Please, choose {inputType}:";
            Console.WriteLine(information);
        }

        public void PrintMsg(string message)
        {
            Console.WriteLine(message);
        }

        private int ConvertNumberToLetter()
        {
            return 65;
        }

        public void WhichShipIsPlayerPlacing(int i)
        {
            switch (i)
            {
                case 1:
                    Console.WriteLine("Placing Carrier (1 square)...");
                    break;
                case 2:
                    Console.WriteLine("Placing Cruiser (2 squares)...");
                    break;
                case 3:
                    Console.WriteLine("Placing Battleship (3 squares)...");
                    break;
                case 4:
                    Console.WriteLine("Placing Submarine (4 squares)...");
                    break;
                case 5:
                    Console.WriteLine("Placing Destroyer (5 squares)...");
                    break;
            }
        }

        public void BoardDisplay(Board board, int player)
        {
            int counterRow = ConvertNumberToLetter();
            int numberOfRows = board.Size;
            int numberOfColumns = numberOfRows;
            string strNumbers = "  ";
            for (int i = 1; i <= numberOfColumns; i++) //
            {
                strNumbers += i.ToString() + " ";
            }
            Console.WriteLine(strNumbers);

            for (int i = 0; i < numberOfRows; i++)
            {
                string counterLetter = ((char)counterRow).ToString();
                DefaultConsoleColor();
                Console.Write($"{counterLetter} ");
                for (int j = 0; j < numberOfColumns; j++)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    ColorsForDifferentSignes(board, i, j);
                    DrawOneSign(board, i, j, player);
                    DefaultConsoleColor();
                }

                counterRow++;
                Console.Write("\n");
            }

            DefaultConsoleColor();
        }

        public void WhoIsOwnerOfBoard(int player)
        {
            Console.WriteLine($"Player{player} board:");
        }

        private void DrawOneSign(Board board, int i, int j, int player)
        {
            if (player != board.Owner)
            {
                if (board.board[i, j].SquareStatus == SquareStatus.Ship)
                {
                    Console.Write('.');
                }
                else
                {
                    Console.Write((char)board.board[i, j].SquareStatus);
                }
            }
            else
            {
                Console.Write((char)board.board[i, j].SquareStatus);
            }
            
            Console.Write(" ");
        }

        public void CurrentPlayerDisplay(int player)
        {
            Console.WriteLine($"Player {player} turn!");
        }

        public void ManualOrRandomShipPlacement()
        {
            string message = "Choose one option: \n1. Manual Ships placement\n2. Random Ships Placement";
            Console.WriteLine(message);
        }
        private void DefaultConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private void ColorsForDifferentSignes(Board board, int i, int j)
        {
            if (board.board[i, j].SquareStatus == SquareStatus.Empty)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (board.board[i, j].SquareStatus == SquareStatus.Hit)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (board.board[i, j].SquareStatus == SquareStatus.Sunk)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            else if (board.board[i, j].SquareStatus == SquareStatus.Missed)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
        }
    }
}

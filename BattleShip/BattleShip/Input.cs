using System;
using System.Security.Cryptography.X509Certificates;

namespace BattleShip
{
    internal class Input
    {
        Display display = new Display();
        public string ProvideInput()
        {
            string playerChoice = Console.ReadLine();
            return playerChoice;
        }

        public bool IsValidMenuInput(string input)
        {
            if (int.TryParse(input, out int chosenNumber) && chosenNumber >= 0 && chosenNumber < 5)
                return true;
            return false;
        }

        private bool IsValidManualOrRandomShipPlacement(string input)
        {
            if (int.TryParse(input, out int chosenNumber) && (chosenNumber == 2 || chosenNumber == 1))
                return true;
            return false;
        }

        public int ChooseManualOrRandomShipPlacement(int player)
        {
            Display display = new Display();
            display.CurrentPlayerDisplay(player);
            display.ManualOrRandomShipPlacement();
            while (true)
            {
                string playerChoice = ProvideInput();
                if (IsValidManualOrRandomShipPlacement(playerChoice))
                {
                    return int.Parse(playerChoice);
                }

                display.PrintMsg("Wrong input. Try again.");
            }

        }

        public bool IsValidCoordinatesInput(int rowCoordinate, int colCoordinate)
        {
            Display display = new Display();
            Board board = new Board();
            if (rowCoordinate >= 0 && rowCoordinate < board.Size &&
                colCoordinate >= 0 && colCoordinate < board.Size)
            {
                return true;
            }
            return false;
        }

        public (int, int) PlayerCoordinatesInputConvert()
        {
            
            int rowCoordinate = -1;
            int colCoordinate = -1;
            while (rowCoordinate == -1 && colCoordinate == -1)
            {
                display.PrintMsg("Provide coordinates: ");
                string playerChoice = ProvideInput();
                (rowCoordinate, colCoordinate) = PlayerChoiceToCoordinates(playerChoice);
                if (!IsValidCoordinatesInput(rowCoordinate, colCoordinate))
                {
                    display.PrintMsg("Wrong coordinates!");
                    rowCoordinate = -1;
                    colCoordinate = -1;
                }

            }
            return (rowCoordinate, colCoordinate);
        }


        private (int, int) PlayerChoiceToCoordinates(string playerChoice)
        {
            if (playerChoice.Length >= 2)
            {
                int rowCoordinate = RowCoordinateConvert(playerChoice);
                int colCoordinate = ColCoordinateConvert(playerChoice);
                return (rowCoordinate, colCoordinate);
            }
            
            return (-1, -1);
        }

        private int RowCoordinateConvert(string playerChoice)
        {
            if (Char.IsLetter(playerChoice, 0))
            {
                return (int)playerChoice[0] % 32 - 1;
            }
           
            return -1;
        }

        private int ColCoordinateConvert(string playerChoice)
        {
            if (int.TryParse(playerChoice.Substring(1), out int colCoordinate))
            {
                return colCoordinate - 1;
            }

            return -1;
        }

        public int playerPlacementOptions(bool right, bool down, bool left, bool up)
        {
            again:
            display.PrintMsg("Your placement options are:");
            if (right) display.PrintMsg("Right = 1");
            if (up) display.PrintMsg("Up = 2");
            if (left) display.PrintMsg("Left = 3");
            if (down) display.PrintMsg("Down = 4");
            
            string playerChoice = ProvideInput();
            int choice = -1;
            if (playerChoice.Length == 1)
            {
                if (int.TryParse(playerChoice.Substring(0), out choice) && choice > 0 && choice < 5)
                {
                    switch (choice)
                    {
                        case 1:
                            if (right)
                            {
                                return 1;
                            } else {goto again;}
                            break;
                        case 2:
                            if (up)
                            {
                                return 2;
                            } else {goto again;}
                            break;
                        case 3:
                            if (left)
                            {
                                return 3;
                            } else {goto again;}
                            break;
                        case 4:
                            if (down)
                            {
                                return 4;
                            } else {goto again;}
                            break;

                    }
                }
                else
                {
                    display.PrintMsg("Wrong input!");
                    goto again;
                };
            }
            else
            {
                display.PrintMsg("Wrong length!");
                goto again;
            }
            

            return choice;
        }
    }
}

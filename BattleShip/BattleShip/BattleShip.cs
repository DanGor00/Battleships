using System.Xml.Serialization;

namespace BattleShip
{
    /// <summary>
    /// This is the main class of program which contains Main method
    /// </summary>
    public class BattleShip
    {

        private static readonly Display Display = new Display();
        private static readonly Input Input = new Input();
        private static readonly BoardFactory BoardFactory = new BoardFactory();
        /// <summary>
        /// This is the entry point of program.
        /// </summary>
        public static void Main()
        {
            Game game = new Game();
            bool playAgain = true;
            while (playAgain)
            {
                int chosenOption = MainMenuHandler();
                ChosenGameModeSwitch(chosenOption);
                playAgain = game.PlayAgain();
            }

        }

        /// <summary>
        /// This is the main menu display and player can choose game mode.
        /// </summary>
        private static int MainMenuHandler()
        {
            bool correctInputProvided = false;
            string playerChoice = "";
            while (!correctInputProvided)
            {
                Display.DisplayMainMenu();
                Display.DisplayProvideInput("number");
                playerChoice = Input.ProvideInput();
                correctInputProvided = Input.IsValidMenuInput(playerChoice);
                Console.Clear();
            }

            return int.Parse(playerChoice);

        }

        /// <summary>
        /// This is the game mode switch that depends on player choice.
        /// </summary>
        private static void ChosenGameModeSwitch(int option)
        {
            switch (option)
            {
                case 1:

                    Game game = new Game();
                    game.RoundPlayerVsPlayer();

                    break;
                case 2:

                    break;
                case 3:
                    Console.WriteLine("Options");
                    break;
                case 4:
                    string message = Game.ScoreBoard.MessageForScoreBoard();
                    Display.PrintMsg(message);
                    break;
                case 0:
                    Console.WriteLine("The End");
                    break;
            }
        }

    }

}
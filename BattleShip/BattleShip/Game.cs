using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace BattleShip
{
    internal class Game
    {

        private readonly Display _display = new Display();
        private readonly Input _input = new Input();
        private int CurrentPlayer { get; set; }
        private readonly Player _player1 = new Player();
        private readonly Player _player2 = new Player();
        private readonly Board _boardPlayer1 = new Board();
        private readonly Board _boardPlayer2 = new Board();
        public static ScoreBoard ScoreBoard = new ScoreBoard();



        public void RoundPlayerVsPlayer()
        {
            InitializeGame();
            RoundPlacementPhase();
            bool hasWon = false;
            while (!hasWon)
            {
                hasWon = RoundShootingPhase();
            }
            
        }

        private bool RoundShootingPhase()
        {
            bool isHit = true;
            while (isHit)
            {
                isHit = ShootingPhase(_boardPlayer1, _boardPlayer2, CurrentPlayer, _player2, _player1);
                _player1.Score--;
                if (!_player1.IsAlive(_player1))
                {
                    EndGame(_boardPlayer1, _boardPlayer2, CurrentPlayer, _player1.Score);
                    return true;
                }
            }
           

            SwitchPlayerMessage();
            CurrentPlayer = SwitchPlayers(CurrentPlayer);

            isHit = true;
            while (isHit)
            {
                isHit = ShootingPhase(_boardPlayer2, _boardPlayer1, CurrentPlayer, _player1, _player2);
                _player2.Score--;
                if (!_player2.IsAlive(_player2))
                {
                    EndGame(_boardPlayer2, _boardPlayer1, CurrentPlayer, _player2.Score);
                    return true;
                }
            }

            SwitchPlayerMessage();
            CurrentPlayer = SwitchPlayers(CurrentPlayer);
            return false;
        }

        private void RoundPlacementPhase()
        {
            PlacementPhase(_boardPlayer1, CurrentPlayer, _player2);
            SwitchPlayerMessage();
            CurrentPlayer = SwitchPlayers(CurrentPlayer);
            PlacementPhase(_boardPlayer2, CurrentPlayer, _player1);
            SwitchPlayerMessage();
            CurrentPlayer = SwitchPlayers(CurrentPlayer);
        }

        private void InitializeGame()
        {
            _player1.Name = 1;
            _player2.Name = 2;
            _player1.Score = 115;
            _player2.Score = 115;
            _player1.Ships = new List<Ship>();
            _player2.Ships = new List<Ship>();
            _boardPlayer1.CreatePlayerBoard();
            _boardPlayer1.Owner = _player1.Name;
            _boardPlayer2.CreatePlayerBoard();
            _boardPlayer2.Owner = _player2.Name;
            CurrentPlayer = _player1.Name;
        }
        private void SwitchPlayerMessage()
        {
            Console.Clear();
            _display.PrintMsg("SWITCH PLAYER. PRESS ENTER TO CONTINUE.");
            _input.ProvideInput();
            Console.Clear();

        }
        private void EndGame(Board board1, Board board2, int player, int score)
        {
            Console.Clear();
            DisplayBothBoards(board1, board2, player);
            _display.PrintMsg($"CONGRATULATION! PLAYER{player} WON!");
            _display.PrintMsg($"PLAYER{player} SCORE: {score}!");
            _display.PrintMsg("Input your name for scoreboard: ");
            string playersNameForScoreBoard = _input.ProvideInput();
            Game.ScoreBoard.HighScores.Add(playersNameForScoreBoard, score);

        }

        public bool PlayAgain()
        {

            _display.PrintMsg("Do you want to play again? (y/n)");
            while (true)
            {
                string playerChoice = _input.ProvideInput();
                if (playerChoice.ToLower() == "n")
                {
                    _display.PrintMsg("THE END");
                    return false;
                }
                if (playerChoice.ToLower() == "y")
                {
                    Console.Clear();
                    return true;
                }

                _display.PrintMsg("Wrong input. Try again.");
            }
        }

        private void PlacementPhase(Board board, int player, Player playerObj)
        {
            Display display = new Display();
            BoardFactory boardFactory = new BoardFactory();
            int manualOrRandomChoice = _input.ChooseManualOrRandomShipPlacement(CurrentPlayer);
            switch (manualOrRandomChoice)
            {
                case 1:
                    ManualPlacementPhase(boardFactory, board, player, playerObj);
                    break;
                case 2:
                    RandomPlacementPhase(boardFactory, board, player, playerObj);
                    Console.Clear();
                    display.BoardDisplay(board, player);
                    Console.ReadLine();
                    break;
            }
        }

        private void ManualPlacementPhase(BoardFactory boardFactory, Board board, int player, Player playerObj)
        {
            for (int i = 1; i < 6; i++)
            {
                Console.Clear();
                _display.BoardDisplay(board, player);
                _display.WhichShipIsPlayerPlacing(i);
                List<(int, int)> shipsCoordinates = boardFactory.MyManualPlacement(i, board);
                
                List<(int, int)> shipCoordinatesStartPostition = new List<(int, int)>();
                for (int j = 0; j < shipsCoordinates.Count; j++)
                {
                    shipCoordinatesStartPostition.Add(shipsCoordinates[j]);
                }
                foreach ((int, int) coordinate in shipsCoordinates)
                {
                    board.board[coordinate.Item1, coordinate.Item2].SquareStatus = SquareStatus.Ship;
                }
                Ship ship = new Ship(ShipType.Carrier);
                AddNewShip(ship, i, shipsCoordinates, shipCoordinatesStartPostition, playerObj);

                // add all coordinates to that ship
            }
        }

        private void AddNewShip(Ship ship, int i, List<(int, int)> shipCoordinates, List<(int, int)> shipCoordinatesStartPostition, Player player)
        {
            switch (i)
            {
                case 1:
                    ship = new Ship(ShipType.Carrier);
                    ship.ShipCoordinates = shipCoordinates;
                    ship.ShipCoordinatesStartPosition = shipCoordinatesStartPostition;
                    break;
                case 2:
                    ship = new Ship(ShipType.Cruiser);
                    ship.ShipCoordinates = shipCoordinates;
                    ship.ShipCoordinatesStartPosition = shipCoordinatesStartPostition;
                    break;
                case 3:
                    ship = new Ship(ShipType.Battleship);
                    ship.ShipCoordinates = shipCoordinates;
                    ship.ShipCoordinatesStartPosition = shipCoordinatesStartPostition;
                    break;
                case 4:
                    ship = new Ship(ShipType.Submarine);
                    ship.ShipCoordinates = shipCoordinates;
                    ship.ShipCoordinatesStartPosition = shipCoordinatesStartPostition;
                    break;
                case 5:
                    ship = new Ship(ShipType.Destroyer);
                    ship.ShipCoordinates = shipCoordinates;
                    ship.ShipCoordinatesStartPosition = shipCoordinatesStartPostition;
                    break;
            }
            player.Ships.Add(ship);
        }

        private void RandomPlacementPhase(BoardFactory boardFactory, Board board, int player, Player playerObj)
        {
            for (int i = 1; i < 6; i++)
            {
                
                List<(int, int)> shipsCoordinates = boardFactory.RandomPlacement(board.Size, i, board);
                List<(int, int)> shipCoordinatesStartPostition = new List<(int, int)>();
                for (int j = 0; j < shipsCoordinates.Count; j++)
                {
                    shipCoordinatesStartPostition.Add(shipsCoordinates[j]);
                }

                foreach ((int, int) coordinate in shipsCoordinates)
                {
                    board.board[coordinate.Item1, coordinate.Item2].SquareStatus = SquareStatus.Ship;
                }
                Ship ship = new Ship(ShipType.Carrier);
                AddNewShip(ship, i, shipsCoordinates, shipCoordinatesStartPostition, playerObj);
            }

        }

        private bool ShootingPhase(Board board1, Board board2, int shootingPlayer, Player player, Player player2)
        {
            DisplayBothBoards(board1, board2, shootingPlayer);
            bool isPlaceShotAlready = true;
            int rowCoordinate = -1;
            int colCoordinate = -1;
            while (isPlaceShotAlready)
            {
                (rowCoordinate, colCoordinate) = _input.PlayerCoordinatesInputConvert();
                //isPlaceShotAlready = player.WasShotBefore('x');
                isPlaceShotAlready = player.WasShotBefore(board2, (rowCoordinate, colCoordinate));
            }
            bool isHit = player.ShootMechanic(board2, (rowCoordinate, colCoordinate), player, player2);
            if (isHit)
            {
                _display.PrintMsg("You hit a ship. You can attack again! Press enter.");
                _input.ProvideInput();
                return true;
            }
            Console.Clear();
            DisplayBothBoards(board1, board2, shootingPlayer);
            _input.ProvideInput();
            return false;


        }


        private void DisplayBothBoards(Board board1, Board board2, int player)
        {
            Console.Clear();
            _display.WhoIsOwnerOfBoard(player);
            _display.BoardDisplay(board1, player);
            player = SwitchPlayers(player);
            _display.WhoIsOwnerOfBoard(player);
            player = SwitchPlayers(player);
            _display.BoardDisplay(board2, player);
            _display.CurrentPlayerDisplay(player);
        }

        public int SwitchPlayers(int player)
        {
            if (player == 1)
            {
                return 2;
            }
            return 1;

        }
    }
}

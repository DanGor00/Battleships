using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class ScoreBoard
    {
        public Dictionary<string, int> HighScores { get; set; }

        public string MessageForScoreBoard()
        {
            string message = "";
            foreach ((string name, int score) in HighScores.OrderByDescending(key => key.Value))
            {
                message += $"{name}: {score}\n";
            }

            return message;
        }
        public ScoreBoard()
        {
            HighScores = new Dictionary<string, int>();
        }
    }
}

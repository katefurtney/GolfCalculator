using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCalculator2
{
    public class Game
    {
        // Game contains Player list and round list
        public List<Player> Players = new();
        public List<Round> Rounds = new();
    }

    public class Round
    {
        // contains list of scores
        public int RoundNumber { get; set; }
        public List<Score> Scores = new();
        // add score
        public void AddScore(Score score)
        {
            Scores.Add(score);
        }
        public void UpdateScore(Score score, int index)
        {
            Scores[index] = score;
        }
        public int GetScoreValue(int index)
        {
            return Scores[index].PlayerScore;
        }
    }

    public class Player
    {
        public string Name { get; set; } = "";
        public int PlayerNum { get; set; }
        public int TotalScore { get; set; } = 0;
    }

    public class Score
    {
        // contains player and int
        public int PlayerScore { get; set; } = 0;
        public Player? player { get; set; }
    }
}


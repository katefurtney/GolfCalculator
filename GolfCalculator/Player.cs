using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCalculator
{
    public class Round
    {
        public int RoundNumber { get; set; } = 0;
        public int Score { get; set; } = 0;
    }
    public class Player
    {
        public string Name { get; set; } = "";
        public int? PlayerNum { get; set; }
        public int TotalScore { get; set; } = 0;
        public List<Round> Scores = new();
        public void AddRound(Round round)
        {
            Scores.Add(round);
        }
    }
}

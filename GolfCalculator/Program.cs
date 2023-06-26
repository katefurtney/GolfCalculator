using static System.Formats.Asn1.AsnWriter;
using System.Text;
using System.Numerics;

namespace GolfCalculator
{
    class Program
    {
        static void Main()
        {
            // create container to hold people
            List<Player> players = new List<Player>();
            List<Player> sortedPlayers = new List<Player>();

            bool valid = true;
            int count = 0;
            do
            {
                Player player = new();
                count++;
                // read in people, press enter
                Console.Write($"\nEnter the name of player {count} or press ENTER: ");
                string? line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    valid = false;
                    continue;
                }
                player.Name = line;
                player.PlayerNum = count;

                // add player to list
                players.Add(player);
            } while (valid);

            // start of a new round
            // add score for each player
            bool endRound;
            bool endGame = false;
            int roundCount = 0;
            do
            {
                roundCount++;
                endRound = false;
                while (!endRound)
                {
                    // create new round object
                    Round round = new();
                    Console.WriteLine($"\nRound {roundCount}.");
                    foreach(Player p in players)
                    {
                        try
                        {
                            Console.WriteLine($"{p.PlayerNum}. {p.Name} - {p.Scores[roundCount - 1].Score}");
                        }
                        catch (System.ArgumentOutOfRangeException)
                        {
                            Console.WriteLine($"{p.PlayerNum}. {p.Name} - {round.Score}");
                        }
                    }
                    Console.WriteLine("Press player number to update score. Press ENTER to end round.");
                    string? line = Console.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        endRound = true;
                        continue;
                    }

                    int num; bool intResultTryParse = Int32.TryParse(line.Substring(0, 1), out num);

                    // update score for that player number
                    Player player = players[num - 1];

                    round.RoundNumber = roundCount;
                    bool validScore;
                    int score;
                    do
                    {
                        validScore = true;
                        Console.Write($"Enter the score for player {num}: ");
                        string? val = Console.ReadLine();
                        score = Convert.ToInt32(val);
                        if (score < -20 || score > 60)
                        {
                            Console.WriteLine($"Invalid score, please re-enter score for {player.Name}: ");
                            validScore = false;
                        }
                    } while (!validScore);
                    round.Score = score;
                    if (player.Scores.Count < roundCount)
                        player.AddRound(round);
                    else
                        player.Scores[roundCount - 1] = round;
                    //// update the players total score
                    //player.TotalScore += round.Score;
                }
                //output round totals
                Console.WriteLine($"The scores for Round {roundCount}:");
                foreach (Player p in players)
                {
                    int totalScore = 0;
                    for (int i = 0; i < p.Scores.Count; ++i)
                    {
                        totalScore += p.Scores[i].Score;
                    }
                    Console.WriteLine($"{p.PlayerNum}. {p.Name}: {totalScore}");
                }
                if (roundCount == 9)
                    endGame = true;
            } while (!endGame);

            sortedPlayers = players;
            sortedPlayers.Sort((x, y) => y.TotalScore.CompareTo(x.TotalScore));
            // output game totals
            Console.WriteLine("\nGame Totals:");
            int rank = 0;
            foreach (Player p in players)
            {
                for (int i = 0; i < p.Scores.Count; ++i)
                {
                    p.TotalScore += p.Scores[i].Score;
                }
                rank++;
                // sum scores for each player
                Console.WriteLine($"{rank}. {p.Name}: {p.TotalScore}");
            }
        }
    }
}
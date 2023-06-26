namespace GolfCalculator2
{
    class Program
    {
        static void Main()
        {
            // create container to hold people
            Game game = new Game();

            // populate rounds with default values
            for (int i = 0; i < 9; i++)
            {
                Round r = new Round();
                r.RoundNumber = i + 1;
                game.Rounds.Add(r);
            }    

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
                game.Players.Add(player);
            } while (valid);

            // initialize scores to zero for each player
            foreach(Round r in game.Rounds)
            {
                foreach (Player p in game.Players)
                {
                    Score s = new();
                    s.PlayerScore = 0;
                    s.player = p;
                    r.AddScore(s);
                }
            }

            // for each player populate that number of scores per round
            bool endRound;
            bool endGame = false;
            int roundCount = 0;
            do
            {
                roundCount++;
                endRound = false;
                while (!endRound)
                {
                    Console.WriteLine($"\nRound {roundCount}.");
                    foreach(Player p in game.Players)
                    {
                        Console.WriteLine($"{p.PlayerNum}. {p.Name} - {game.Rounds[roundCount - 1].GetScoreValue(p.PlayerNum - 1)}");
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
                    Player player = game.Players[num - 1];

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
                    Score newScore = new Score();
                    newScore.player = player;
                    newScore.PlayerScore = score;
                    game.Rounds[roundCount - 1].UpdateScore(newScore, num - 1);

                }
                //output round totals
                Console.WriteLine($"The scores after Round {roundCount}:");
                foreach (Score s in game.Rounds[roundCount - 1].Scores)
                {
                    s.player.TotalScore += s.PlayerScore;
                }
                // sort players
                List<Player> sortedRound = new List<Player>(game.Players);
                sortedRound.Sort((x, y) => x.TotalScore.CompareTo(y.TotalScore));
                for (int i = 0; i < sortedRound.Count; ++i)
                {
                    Console.WriteLine($"{i + 1}. {sortedRound[i].Name} - {sortedRound[i].TotalScore}");
                }
                if (roundCount == 9)
                    endGame = true;
            } while (!endGame);

            // sort players
            List<Player> sortedGame = game.Players;
            sortedGame.Sort((x, y) => x.TotalScore.CompareTo(y.TotalScore));

            // output game totals
            Console.WriteLine("\nGame Totals:");
            for (int i = 0; i < sortedGame.Count; ++i)
            {
                Console.WriteLine($"{i + 1}. {sortedGame[i].Name} - {sortedGame[i].TotalScore}");
            }
        }// end main
    }
}
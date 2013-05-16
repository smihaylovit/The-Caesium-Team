using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesium.BullsAndCows
{
    public class ScoreBoard
    {
        const int topRecordsNumber = 5;
        public List<ScoreRecord> TopRecords { get; private set; }

        // TODO fix this method, duplicates with DoTopScore from Program class
        public ScoreBoard()
        {
            this.TopRecords = new List<ScoreRecord>(topRecordsNumber);
        }

        public static void ShowScoreBoard(GameEngine currentGame, ScoreBoard currentScoreBoard)
        {
            if (currentGame.numberOfCheats != 0)
            {
                Console.WriteLine("Congratulations! You guessed the secret number in " + currentGame.numberOfGuesses +
                              " attempts and " + currentGame.numberOfCheats + " cheats.");
                Console.WriteLine("You are not allowed to enter the top scoreboard.");
                Console.WriteLine(currentScoreBoard.ToString());
            }
            else
            {
                if (currentScoreBoard.TopRecords.Count < 5)
                {
                    Console.WriteLine("Congratulations! You guessed the secret number in " + currentGame.numberOfGuesses + " attempts.");
                    Console.Write("Please enter your name for the top scoreboard: ");

                    string name = Console.ReadLine();

                    currentScoreBoard.TopRecords.Add(new ScoreRecord(name, currentGame.numberOfGuesses));
                    currentScoreBoard.TopRecords.Sort();
                    Console.WriteLine(currentScoreBoard.ToString());
                }
                else if (currentGame.numberOfGuesses >= currentScoreBoard.TopRecords[currentScoreBoard.TopRecords.Count - 1].Score)
                {
                    Console.WriteLine("Congratulations! You guessed the secret number in " + currentGame.numberOfGuesses + " attempts.");
                    Console.WriteLine("You are not allowed to enter the top scoreboard.");
                    Console.WriteLine(currentScoreBoard.ToString());
                }
                else
                {
                    Console.WriteLine("Congratulations! You guessed the secret number in " + currentGame.numberOfGuesses + " attempts.");
                    Console.Write("Please enter your name for the top scoreboard: ");

                    string name = Console.ReadLine();

                    currentScoreBoard.TopRecords.RemoveAt(currentScoreBoard.TopRecords.Count - 1);
                    currentScoreBoard.TopRecords.Add(new ScoreRecord(name, currentGame.numberOfGuesses));
                    currentScoreBoard.TopRecords.Sort();
                    Console.WriteLine(currentScoreBoard.ToString());
                }
            }
        }

        public override string ToString()
        {
            if (this.TopRecords.Count == 0)
            {
                return "Top scoreboard is empty.";
            }
            else
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine("---------- Scoreboard ----------");
                for (int index = 0; index < this.TopRecords.Count; index++)
                {
                    result.AppendLine((index + 1) + ". " + this.TopRecords[index].ToString());
                }
                result.AppendLine("--------------------------------");
                return result.ToString();
            }
        }
    }
}

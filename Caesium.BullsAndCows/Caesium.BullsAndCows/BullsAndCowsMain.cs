using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesium.BullsAndCows
{
//Helloween sa mnogo zdravi, yaaaaaaaaaaahh snoshti biah na koncerta!!!!!!!!!!!!
    public delegate void TopScoresDelegate(GameEngine g, ScoreBoard board);
    class BullsAndCowsMain
    {
        // TODO move this method to the ScoreBoard class
        private static void ShowScoreBoard(GameEngine currentGame, ScoreBoard currentScoreBoard)
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

        static void Main()
        {
            ScoreBoard currentScoreBoard = new ScoreBoard();
            while (new GameEngine(currentScoreBoard, ShowScoreBoard).Run()) { }
        }
    }
}

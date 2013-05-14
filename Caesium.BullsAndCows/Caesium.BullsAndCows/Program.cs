using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesium.BullsAndCows
{
//Helloween sa mnogo zdravi, yaaaaaaaaaaahh snoshti biah na koncerta!!!!!!!!!!!!
    public delegate void TopScoresDelegate(Game g, ScoreBoard board);
    class Program
    {
        // TODO move this method to the ScoreBoard class
        private static void DoTopScores(Game currentGame, ScoreBoard board)
        {
            if (currentGame.numberOfCheats != 0)
            {
                Console.WriteLine("Congratulations! You guessed the secret number in " + currentGame.numberOfGuesses + 
                              " attempts and " + currentGame.numberOfCheats + " cheats.");
                Console.WriteLine("You are not allowed to enter the top scoreboard.");
                Console.WriteLine(board.ToString());
            }
            else
            {
                if (board.TopRecords.Count < 5)
                {
                    Console.WriteLine("Congratulations! You guessed the secret number in " + currentGame.numberOfGuesses + " attempts.");
                    Console.Write("Please enter your name for the top scoreboard: ");

                    string name = Console.ReadLine();

                    board.TopRecords.Add(new Record(name, currentGame.numberOfGuesses));
                    board.TopRecords.Sort();
                    Console.WriteLine(board.ToString());
                }
                else if (currentGame.numberOfGuesses >= board.TopRecords[board.TopRecords.Count - 1].Score)
                {
                    Console.WriteLine("Congratulations! You guessed the secret number in " + currentGame.numberOfGuesses + " attempts.");
                    Console.WriteLine("You are not allowed to enter the top scoreboard.");
                    Console.WriteLine(board.ToString());
                }
                else
                {
                    Console.WriteLine("Congratulations! You guessed the secret number in " + currentGame.numberOfGuesses + " attempts.");
                    Console.Write("Please enter your name for the top scoreboard: ");

                    string name = Console.ReadLine();

                    board.TopRecords.RemoveAt(board.TopRecords.Count - 1);
                    board.TopRecords.Add(new Record(name, currentGame.numberOfGuesses));
                    board.TopRecords.Sort();
                }
            }
        }

        static void Main()
        {
            ScoreBoard board = new ScoreBoard();
            while (new Game(board, DoTopScores).Run()) { }
        }
    }
}

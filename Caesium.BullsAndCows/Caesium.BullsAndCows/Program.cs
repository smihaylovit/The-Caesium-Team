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
            if (currentGame.score != -1 && currentGame.score < board.topRecords[4].Score)
            {
                Console.Write("TOP SCORE! Please enter your name:");
                string name = Console.ReadLine();

                List<Record> list = new List<Record>(board.topRecords);
                list.Add(new Record(name, currentGame.score));
                list.Sort();
                for (int i = 0; i < 5; i++)
                    board.topRecords[i] = list[i];
            }
        }

        static void Main()
        {
            ScoreBoard board = new ScoreBoard();
            while (new Game(board, DoTopScores).Run()) { }
        }
    }
}

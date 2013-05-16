using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesium.BullsAndCows
{
    public delegate void TopScoresDelegate(GameEngine g, ScoreBoard board);
    class BullsAndCowsMain
    {
        static void Main()
        {
            ScoreBoard currentScoreBoard = new ScoreBoard();
            while (new GameEngine(currentScoreBoard, ScoreBoard.ShowScoreBoard).Run()) { }
        }
    }
}

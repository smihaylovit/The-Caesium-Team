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
       

        static void Main()
        {
            ScoreBoard currentScoreBoard = new ScoreBoard();
            while (new GameEngine(currentScoreBoard, ScoreBoard.ShowScoreBoard).Run()) { }
        }
    }
}

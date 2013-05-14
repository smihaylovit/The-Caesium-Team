using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesium.BullsAndCows
{
    public class ScoreBoard
    {
        internal List<Record> topRecords = new List<Record>();

        // TODO fix this method, duplicates with DoTopScore from Program class
        public ScoreBoard()
        {
            for (int i = 0; i < 5; i++)
            {
                //topRecords[i] = new Record("Unknown", int.MaxValue);
                topRecords.Add( new Record("Unknown", int.MaxValue));
            }

        }

        public void Output()
        {
            Console.WriteLine("----Scoreboard----");
            //Console.WriteLine("1.(" + topRecords[0].Score + ")" + topRecords[0].Name);
            //Console.WriteLine("2.(" + topRecords[1].Score + ")" + topRecords[1].Name);
            //Console.WriteLine("3.(" + topRecords[2].Score + ")" + topRecords[2].Name);
            //Console.WriteLine("4.(" + topRecords[3].Score + ")" + topRecords[3].Name);
            //Console.WriteLine("5.(" + topRecords[4].Score + ")" + topRecords[4].Name);
            foreach (var record in topRecords)
            {
                int i = 1;
                Console.WriteLine(i + ". " + record.ToString());
                i++;
            }
            Console.WriteLine("------------------");
        }
    }
}

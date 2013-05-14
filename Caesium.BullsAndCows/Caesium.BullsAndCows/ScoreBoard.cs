using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesium.BullsAndCows
{
    public class ScoreBoard
    {
        const int topRecordsNumber = 5;
        public List<Record> TopRecords { get; private set; }

        // TODO fix this method, duplicates with DoTopScore from Program class
        public ScoreBoard()
        {
            this.TopRecords = new List<Record>(topRecordsNumber);
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

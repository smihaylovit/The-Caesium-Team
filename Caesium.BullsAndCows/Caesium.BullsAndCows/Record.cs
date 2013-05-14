using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesium.BullsAndCows
{
    public class Record : IComparable<Record>
    {
        private string name;
        private int score;     
    
        public Record(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public int Score
        {
            get
            {
                return this.score;
            }
        }

        public int CompareTo(Record otherRecord)
        {
            return this.score.CompareTo(otherRecord.score);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(this.Name);
            result.Append(" ----> ");
            result.Append(this.Score);
            result.Append(" guesses.");

            return result.ToString();
        }
    }
}

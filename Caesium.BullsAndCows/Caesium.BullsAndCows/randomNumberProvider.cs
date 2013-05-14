using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kravi
{
    public class randomNumberProvider
    {
        // TODO make this class to just generate a random number (1000-9999) no need for a derived class (MyProvider.cs)
        protected Random r = new Random();
        private static randomNumberProvider currentProvider;
        public static randomNumberProvider CurrentProvider
        {
            get
            {
                if (currentProvider == null)
                {
                    currentProvider = new randomNumberProvider();
                }

                return currentProvider;
            }
            set
            {
                currentProvider = value;
            }
        }
        public virtual string GetRandomNumber()
        {
            return 4165.ToString();
        }
    }
}

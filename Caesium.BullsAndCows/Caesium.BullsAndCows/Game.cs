using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//test
namespace Caesium.BullsAndCows
{
    public class Game
    {
        private string secretNumber;
        private List<int> positionsList;
        private int currentPosition = 0;
        internal int score; 
        private static readonly bool shouldContinue = true;
        private static readonly bool shouldNotContinue = false;
        private ScoreBoard myBoard;
        private TopScoresDelegate showTopScores;

        public Game(ScoreBoard bb, TopScoresDelegate doTopScores)
        {
            this.myBoard = bb;
            this.showTopScores = doTopScores;
        }

        // TODO fix random help number generator
        private List<int> Positions
        {
            get
            {
                if (positionsList == null)
                {
                    positionsList = new List<int>();
                    for(int i=0;i<secretNumber.Length;++i)
                    {
                        positionsList.Add(i);
                    }
                    for (int i = 0; i < secretNumber.Length; i++)
                    {
                        int t = int.Parse(randomNumberProvider.CurrentProvider.GetRandomNumber());
                        t = (int)((t - 1000.0) / 9000.0 * secretNumber.Length);
                        int tmp = positionsList[t];
                        positionsList[t] = positionsList[i];
                        positionsList[i] = tmp;
                    }
                }

                return positionsList;
            }
        }

        public bool Run()
        {
            Console.WriteLine("A new game has begun!");
            Init();

            while (true)
            {
                string command = Console.ReadLine();

                switch (command)
                {
                    case "exit":
                        return shouldNotContinue;
                    case "restart":
                        return shouldContinue;
                    case "help":
                        score = -1;
                        ShowRand();
                        break;
                    case "top":
                        myBoard.Output();
                        break;
                    default:
                        if (score != -1)
                        {
                            score++;
                        }

                        if (IsGuessCurrect(command))
                        {
                            this.showTopScores(this, this.myBoard);

                            if (AskForNewGame())
                            {
                                return shouldContinue;
                            }
                            else 
                            {
                                return shouldNotContinue;
                            }
                        }
                        break;
                }
            } 
        }

        private bool AskForNewGame()
        {
            Console.WriteLine("Another game ? (Y/N)");
            string answer = Console.ReadLine();

            if (answer.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsGuessCurrect(string guessedNumber)
        {
            if (guessedNumber == secretNumber)
            {
                Console.WriteLine("HOLYCOW, YOU HAVE WON!");
                return true;
            }

            bool[] foundPositions = new bool[secretNumber.Length];

            int bullsCount = BullsCount(guessedNumber, foundPositions);
            int cowsCount = CowsCount(guessedNumber, foundPositions);

            Console.WriteLine("Wrong number! Bulls: {0}, Cows: {1}", bullsCount, cowsCount);
            return false;
        }

        private int BullsCount(string guessedNumber, bool[] foundBullPosition)
        {
            int bullsCount = 0;

            for (int i = 0; i < secretNumber.Length; i++)
            {
                for (int j = 0; j < guessedNumber.Length; j++)
                {
                    if (secretNumber[i] == guessedNumber[j])
                    {
                        if (i == j)
                        {
                            foundBullPosition[i] = true;
                            bullsCount++;
                        }
                    }
                }
            }

            return bullsCount;
        }

        // Counts how many cows are found in the guessed number
        private int CowsCount(string guessedNumber, bool[] foundCowPositions)
        {
            int cowsCount = 0;

            for (int i = 0; i < secretNumber.Length; i++)
            {
                if (!foundCowPositions[i])
                {
                    bool isCowFound = false;
                    for (int j = 0; j < guessedNumber.Length; j++)
                    {
                        if (secretNumber[i] == guessedNumber[j])
                        {
                            if (i != j)
                            {
                                isCowFound = true;
                            }
                            else
                            {
                                Environment.Exit(-1);
                            }
                        }
                    }
                    if (isCowFound) { cowsCount++; }
                }
            }

            return cowsCount;
        }

        // TODO fix this method
        private void ShowRand()
        {
            int bulatpos = Positions[++currentPosition % secretNumber.Length]+1;
            int bull = secretNumber[Positions[currentPosition % secretNumber.Length]];
            char[] str = new char[] { 'X', 'X', 'X', 'X' };
            str[bulatpos] = (char) bull;
            Console.WriteLine("The number looks like " + str.ToString());
        }

        private void Init()
        {
            //randomNumberProvider.CurrentProvider = new MyProvider();
            secretNumber = new Random().Next(1000, 10000).ToString();
            //randomNumberProvider.CurrentProvider.GetRandomNumber();
            score = 0;
        }
    }
}

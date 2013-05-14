using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesium.BullsAndCows
{
    public class GameEngine
    {
        private string secretNumber;
        private List<int> positionsList;
        private int currentPosition = 0;
        internal int numberOfGuesses;
        internal int numberOfCheats;
        private static readonly bool shouldContinue = true;
        private static readonly bool shouldNotContinue = false;
        private ScoreBoard myBoard;
        private TopScoresDelegate showTopScores;

        public GameEngine(ScoreBoard bb, TopScoresDelegate doTopScores)
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
                        int t = int.Parse(new Random().Next(1000, 10000).ToString());
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
            Console.WriteLine("Welcome to “Bulls and Cows” game. Please try to guess my secret 4-digit number.");
            Console.WriteLine("Use 'top' to view the top scoreboard, 'restart' to start a new game, 'help' to cheat");
            Console.WriteLine("and 'exit' to quit the game.");
            Init();

            while (true)
            {
                Console.Write("Enter your guess or command: ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case "exit":
                        return false;
                    case "restart":
                        return true;
                    case "help":          
                        ShowRand();
                        numberOfCheats += 1;
                        break;
                    case "top":
                        Console.WriteLine(myBoard.ToString());
                        break;
                    default:
                        numberOfGuesses++;

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
            int bulatpos = Positions[++currentPosition % secretNumber.Length];
            char bull = secretNumber[Positions[currentPosition % secretNumber.Length]];
            StringBuilder str = new StringBuilder("XXXX");
            str[bulatpos] = bull;
            Console.WriteLine("The number looks like " + str.ToString());
        }

        private void Init()
        {
            //secretNumber = new Random().Next(1000, 10000).ToString();
            secretNumber = "1234";
            numberOfGuesses = 0;
            numberOfCheats = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesium.BullsAndCows
{
    public class GameEngine
    {
        private string secretNumber;
        internal int numberOfGuesses;
        internal int numberOfCheats;
        private ScoreBoard scoreBoard;
        private TopScoresDelegate showTopScores;

        /// <summary>
        /// Fields for generating random bull position for the help command.
        /// See also GenerateRandomPosition and ShowRand methods.
        /// </summary> 
        private List<int> Positions = new List<int>() { 0, 1, 2, 3 };
        private int maxRandomNumber = 4;

        public GameEngine(ScoreBoard scoreBoard, TopScoresDelegate showScoreBoard)
        {
            this.scoreBoard = scoreBoard;
            this.showTopScores = showScoreBoard;
        }

        /// <summary>
        /// The game user interface.
        /// </summary>
        public bool Run()
        {
            Console.WriteLine("Welcome to \"Bulls and Cows\" game. Please try to guess my secret 4-digit number.");
            Console.WriteLine("Use 'top' to view the top scoreboard, 'restart' to start a new game, 'help' to cheat");
            Console.WriteLine("and 'exit' to quit the game.");
            
            InitializeGame();

            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter your guess or command: ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case "exit":
                        Console.WriteLine("Good Bye!");
                        return false;
                    case "restart":
                        return true;
                    case "help":          
                        PrintRandomBull();
                        numberOfCheats += 1;
                        break;
                    case "top":
                        Console.WriteLine(scoreBoard.ToString());
                        break;
                    default:
                        numberOfGuesses++;

                        if (IsGuessCurrect(command))
                        {
                            this.showTopScores(this, this.scoreBoard);
                            return AskForNewGame();
                        }
                        break;
                }
            } 
        }

        /// <summary>
        /// Asks user for starting a new game.
        /// </summary>
        private bool AskForNewGame()
        {
            Console.WriteLine("Do you want to start a new game? (y/n)");
            string answer = Console.ReadLine();

            if (answer.ToLower() == "y")
            {
                return true;
            }
            else
            {
                Console.WriteLine("Good bye!");
                return false;
            }
        }
        /// <summary>
        /// Checks if secret and guessed number are equal.
        /// </summary>
        private bool IsGuessCurrect(string guessedNumber)
        {
            if (guessedNumber == secretNumber)
            {
                PrintOutput();
                return true;
            }

            bool[] foundPositions = new bool[secretNumber.Length];

            int bullsCount = BullsCount(guessedNumber, foundPositions);
            int cowsCount = CowsCount(guessedNumber, foundPositions);

            Console.WriteLine("Wrong number! Bulls: {0}, Cows: {1}", bullsCount, cowsCount);
            return false;
        }

        /// <summary>
        /// Prints the output when the player wins.
        /// </summary>
        private void PrintOutput()
        {
            Console.WriteLine();
            Console.WriteLine("HOLYCOW, YOU HAVE WON!");
            Console.Write("Congratulations! You guessed the secret number in ");
            if (numberOfCheats != 0)
            {
                Console.WriteLine(numberOfGuesses + " attempts and " + numberOfCheats + " cheats.");
                Console.WriteLine("You are not allowed to enter the top scoreboard!");
            }
            else
            {
                int topRecordsCount = scoreBoard.TopRecords.Count;
                if (topRecordsCount < 5)
                {
                    Console.WriteLine(numberOfGuesses + " attempts.");
                    Console.Write("Please enter your name for the top scoreboard: ");

                    string name = Console.ReadLine();
                    scoreBoard.TopRecords.Add(new ScoreRecord(name, numberOfGuesses));
                    scoreBoard.TopRecords.Sort();
                }
                else if (numberOfGuesses >= scoreBoard.TopRecords[topRecordsCount - 1].Score)
                {
                    Console.WriteLine(numberOfGuesses + " attempts.");
                    Console.WriteLine("You are not allowed to enter the top scoreboard!");
                }
                else
                {
                    Console.WriteLine(numberOfGuesses + " attempts.");
                    Console.Write("Please enter your name for the top scoreboard: ");

                    string name = Console.ReadLine();
                    scoreBoard.TopRecords.RemoveAt(scoreBoard.TopRecords.Count - 1);
                    scoreBoard.TopRecords.Add(new ScoreRecord(name, numberOfGuesses));
                    scoreBoard.TopRecords.Sort();
                }
            }
        }

        /// <summary>
        /// Counts how many bulls are found in the guessed number
        /// </summary>
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

        /// <summary>
        /// Counts how many cows are found in the guessed number
        /// </summary>
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

        /// <summary>
        /// Generates unique random position every time.
        /// </summary>
        private int GenerateRandomPosition()
        {
            if (maxRandomNumber <= 0)
            {             
                return -1;
            }
            else
            {
                Random rand = new Random();
                int randomNumber = rand.Next(0, maxRandomNumber);
                int generatedNumber = Positions[randomNumber];

                maxRandomNumber--;
                Positions.RemoveAt(randomNumber);
                return generatedNumber;
            }      
        }

        /// <summary>
        /// Reveals a bull on the console on random position.
        /// </summary>
        private void PrintRandomBull()
        {
            int randomPosition = GenerateRandomPosition();
            if (randomPosition == -1)
            {
                Console.WriteLine("You can't call for help anymore!");
            }
            else
            {
                char randomBull = secretNumber[randomPosition];
                StringBuilder result = new StringBuilder("XXXX");
                result[randomPosition] = randomBull;
                Console.WriteLine("The number looks like " + result.ToString());
            }
        }

        /// <summary>
        /// Initializes new game.
        /// </summary>
        private void InitializeGame()
        {
            //secretNumber = new Random().Next(1000, 10000).ToString();
            secretNumber = "1234";
            numberOfGuesses = 0;
            numberOfCheats = 0;
        }
    }
}

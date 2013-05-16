﻿using System;
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
        private ScoreBoard scoreBoard;
        private TopScoresDelegate showTopScores;

        // Fields for generating random bull position for the help command
        // see GenerateRandomPosition and ShowRand methods
        private List<int> Positions = new List<int>() { 0, 1, 2, 3 };
        private int maxRandomNumber = 4;

        public GameEngine(ScoreBoard scoreBoard, TopScoresDelegate showScoreBoard)
        {
            this.scoreBoard = scoreBoard;
            this.showTopScores = showScoreBoard;
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


        // Generates a random position that is unique.
        // You cant get the same position 2 times
        private int GenerateRandomPosition()
        {
            if (maxRandomNumber <= 0)
            {
                throw new Exception("You cant call for help anymore!");
            }

            Random rand = new Random();
            int randomNumber = rand.Next(0, maxRandomNumber);
            int generatedNumber = Positions[randomNumber];

            maxRandomNumber--;
            Positions.RemoveAt(randomNumber);

            return generatedNumber;
        }

        // Prints on the console a random bull
        private void PrintRandomBull()
        {
            int randomPosition = GenerateRandomPosition();
            char bull = secretNumber[randomPosition];
            StringBuilder sb = new StringBuilder("XXXX");
            sb[randomPosition] = bull;
            Console.WriteLine("The number looks like " + sb.ToString());
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
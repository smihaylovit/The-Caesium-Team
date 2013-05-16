using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Caesium.BullsAndCows.Tests
{
    [TestClass]
    public class GameEngineTest
    {
        private TextWriter consoleWriter = File.CreateText("../../output.txt");
        private TextReader consoleReader;
        private StreamWriter commandWriter = new StreamWriter("../../input.txt");
        private StreamWriter expectedWriter = new StreamWriter("../../expected.txt");

        // This method runs BEFORE every test method.
        [TestInitialize]
        public void Initialize()
        {
            // redirect the console output to a file so we can compare them.
            Console.SetOut(consoleWriter);
        }

        // This method allows you to mimic Console.WriteLine
        // what you type in command will be "printed" on the console
        private void WriteCommand(params string[] commands)
        {
            using (commandWriter)
            {
                foreach (var command in commands)
                {
                    commandWriter.WriteLine(command);
                }
            }

            consoleReader = File.OpenText("../../input.txt");
            Console.SetIn(consoleReader);
        }

        [TestMethod]
        public void RunMethodExitTest()
        {
            // give commands to the game
            WriteCommand("1234", "y", "exit");

            // for some reason it doesn't work if we just define a string
            // so we use a txt file to write the expected output
            using (expectedWriter)
            {
                expectedWriter.WriteLine(@"Welcome to ""Bulls and Cows"" game. Please try to guess my secret 4-digit number.
Use 'top' to view the top scoreboard, 'restart' to start a new game, 'help' to cheat
and 'exit' to quit the game.
Enter your guess or command: HOLYCOW, YOU HAVE WON!
Congratulations! You guessed the secret number in 1 attempts.
Please enter your name for the top scoreboard: ---------- Scoreboard ----------
1. y ----> 1 guesses.
--------------------------------

Do you want to start a new game? (y/n)
Good bye!");
            }

            // load the expected output into a string
            string expected = new StreamReader("../../expected.txt").ReadToEnd();

            // start the game
            ScoreBoard currentScoreBoard = new ScoreBoard();
            while (new GameEngine(currentScoreBoard, ScoreBoard.ShowScoreBoard).Run()){ }

            // flush so the output of the console is printed in the file
            consoleWriter.Flush();
            // close the output file so we can read it (we dont need to write in it anymore)
            consoleWriter.Close();
            string output = new StreamReader("../../output.txt").ReadToEnd();
            Assert.AreEqual(expected, output);
            
        }

        
    }
}

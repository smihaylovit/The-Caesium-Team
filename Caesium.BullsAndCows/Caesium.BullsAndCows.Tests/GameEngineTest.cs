using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Caesium.BullsAndCows.Tests
{
    [TestClass]
    public class GameEngineTest
    {
        #region initialize and cleanup
        private static TextWriter consoleWriter;
        private static TextReader consoleReader;
        private static StreamWriter commandWriter;
        private static StreamWriter expectedWriter;
        private static StreamReader expectedReader;
        private static StreamReader outputReader;

        /// <summary>
        /// This method runs BEFORE every test method.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            expectedWriter = new StreamWriter("../../expected.txt");
            // redirect the console output to a file so we can compare them.
            consoleWriter = File.CreateText("../../output.txt");
            Console.SetOut(consoleWriter);
        }

        /// <summary>
        /// This method runs AFTER every test method
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            consoleWriter.Close();
            consoleReader.Close();
            expectedWriter.Close();
            expectedReader.Close();
        }
        #endregion
        
        /// <summary>
        /// This method allows you to mimic Console.WriteLine.
        /// What you type in command will be "printed" on the console.
        /// </summary>
        private void WriteCommand(params string[] commands)
        {
            using (commandWriter = new StreamWriter("../../input.txt"))
            {
                foreach (var command in commands)
                {
                    commandWriter.WriteLine(command);
                }
            }

            consoleReader = File.OpenText("../../input.txt");
            Console.SetIn(consoleReader);
        }

        #region Run Method Tests

        [TestMethod]
        public void RunExitTest()
        {
            WriteCommand("exit");

            using (expectedWriter)
            {
                expectedWriter.WriteLine(@"Welcome to ""Bulls and Cows"" game. Please try to guess my secret 4-digit number.
Use 'top' to view the top scoreboard, 'restart' to start a new game, 'help' to cheat
and 'exit' to quit the game.

Enter your guess or command: Good Bye!");
            }
            // load the expected output into a string
            expectedReader = new StreamReader("../../expected.txt");
            string expected = expectedReader.ReadToEnd();

            // start the game
            ScoreBoard currentScoreBoard = new ScoreBoard();
            while (new GameEngine(currentScoreBoard, ScoreBoard.ShowScoreBoard).Run()) { }

            // flush so the output of the console is printed in the file
            consoleWriter.Flush();
            // close the output file so we can read it (we dont need to write in it anymore)
            consoleWriter.Close();

            outputReader = new StreamReader("../../output.txt");
            string output = outputReader.ReadToEnd();
            outputReader.Close();

            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void RunRightGuessTest()
        {
            // give commands to the game
            WriteCommand("1234", "adrian", "n");

            // for some reason it doesn't work if we just define a string
            // so we use a txt file to write the expected output
            using (expectedWriter)
            {
                expectedWriter.WriteLine(@"Welcome to ""Bulls and Cows"" game. Please try to guess my secret 4-digit number.
Use 'top' to view the top scoreboard, 'restart' to start a new game, 'help' to cheat
and 'exit' to quit the game.

Enter your guess or command: 
HOLYCOW, YOU HAVE WON!
Congratulations! You guessed the secret number in 1 attempts.
Please enter your name for the top scoreboard: 
---------- Scoreboard ----------
1. adrian ----> 1 guesses.
--------------------------------

Do you want to start a new game? (y/n)
Good bye!");
            }

            // load the expected output into a string
            expectedReader = new StreamReader("../../expected.txt");
            string expected = expectedReader.ReadToEnd();

            // start the game
            ScoreBoard currentScoreBoard = new ScoreBoard();
            while (new GameEngine(currentScoreBoard, ScoreBoard.ShowScoreBoard).Run()){ }

            // flush so the output of the console is printed in the file
            consoleWriter.Flush();
            // close the output file so we can read it (we dont need to write in it anymore)
            consoleWriter.Close();
            outputReader = new StreamReader("../../output.txt");
            string output = outputReader.ReadToEnd();
            outputReader.Close();

            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void RunRestartTest()
        {
            WriteCommand("restart", "exit");

            using (expectedWriter)
            {
                expectedWriter.WriteLine(@"Welcome to ""Bulls and Cows"" game. Please try to guess my secret 4-digit number.
Use 'top' to view the top scoreboard, 'restart' to start a new game, 'help' to cheat
and 'exit' to quit the game.

Enter your guess or command: Welcome to ""Bulls and Cows"" game. Please try to guess my secret 4-digit number.
Use 'top' to view the top scoreboard, 'restart' to start a new game, 'help' to cheat
and 'exit' to quit the game.

Enter your guess or command: Good Bye!");
            }
            // load the expected output into a string
            expectedReader = new StreamReader("../../expected.txt");
            string expected = expectedReader.ReadToEnd();

            // start the game
            ScoreBoard currentScoreBoard = new ScoreBoard();
            while (new GameEngine(currentScoreBoard, ScoreBoard.ShowScoreBoard).Run()) { }

            // flush so the output of the console is printed in the file
            consoleWriter.Flush();
            // close the output file so we can read it (we dont need to write in it anymore)
            consoleWriter.Close();

            outputReader = new StreamReader("../../output.txt");
            string output = outputReader.ReadToEnd();
            outputReader.Close();

            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void RunHelpTest()
        {
            WriteCommand("top", "exit");

            using (expectedWriter)
            {
                expectedWriter.WriteLine(@"Welcome to ""Bulls and Cows"" game. Please try to guess my secret 4-digit number.
Use 'top' to view the top scoreboard, 'restart' to start a new game, 'help' to cheat
and 'exit' to quit the game.

Enter your guess or command: Top scoreboard is empty.

Enter your guess or command: Good Bye!");
            }
            // load the expected output into a string
            expectedReader = new StreamReader("../../expected.txt");
            string expected = expectedReader.ReadToEnd();

            // start the game
            ScoreBoard currentScoreBoard = new ScoreBoard();
            while (new GameEngine(currentScoreBoard, ScoreBoard.ShowScoreBoard).Run()) { }

            // flush so the output of the console is printed in the file
            consoleWriter.Flush();
            // close the output file so we can read it (we dont need to write in it anymore)
            consoleWriter.Close();

            outputReader = new StreamReader("../../output.txt");
            string output = outputReader.ReadToEnd();
            outputReader.Close();

            Assert.AreEqual(expected, output);
        }

        #endregion

        #region IsGuessCorrect Tests
        [TestMethod]
        public void IsGuessCorrectWrongGuessTest()
        {
            // give commands to the game
            WriteCommand("1111", "1235", "1143", "exit");

            // for some reason it doesn't work if we just define a string
            // so we use a txt file to write the expected output
            using (expectedWriter)
            {
                expectedWriter.WriteLine(@"Welcome to ""Bulls and Cows"" game. Please try to guess my secret 4-digit number.
Use 'top' to view the top scoreboard, 'restart' to start a new game, 'help' to cheat
and 'exit' to quit the game.

Enter your guess or command: Wrong number! Bulls: 1, Cows: 0

Enter your guess or command: Wrong number! Bulls: 3, Cows: 0

Enter your guess or command: Wrong number! Bulls: 1, Cows: 2

Enter your guess or command: Good Bye!");
            }

            // load the expected output into a string
            expectedReader = new StreamReader("../../expected.txt");
            string expected = expectedReader.ReadToEnd();

            // start the game
            ScoreBoard currentScoreBoard = new ScoreBoard();
            while (new GameEngine(currentScoreBoard, ScoreBoard.ShowScoreBoard).Run()){ }

            // flush so the output of the console is printed in the file
            consoleWriter.Flush();
            // close the output file so we can read it (we dont need to write in it anymore)
            consoleWriter.Close();
            outputReader = new StreamReader("../../output.txt");
            string output = outputReader.ReadToEnd();
            outputReader.Close();

            Assert.AreEqual(expected, output);
        }
        #endregion
    }
}

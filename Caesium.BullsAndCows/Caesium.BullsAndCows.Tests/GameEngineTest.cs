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
            WriteCommand("1234", "y", "exit");
            string expected = new StreamReader("../../expected.txt").ReadToEnd();

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

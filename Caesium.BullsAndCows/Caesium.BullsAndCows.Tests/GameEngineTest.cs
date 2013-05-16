using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Caesium.BullsAndCows.Tests
{
    [TestClass]
    public class GameEngineTest
    {
        [TestInitialize]
        public void Initialize()
        {
            // redirect the console output to a file so we can compare them.
            TextWriter writer = File.CreateText("../../output.txt");
            Console.SetOut(writer);
        }

        [TestMethod]
        public void RunMethodTest()
        {

        }
    }
}

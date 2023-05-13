using NUnit.Framework;
using Battleships.ConsoleWrapper;
using Moq;
using System;
using System.Collections.Generic;

namespace Battleships.EndToEndTests
{
    public class BattleshipsConsoleGameTests_ExeptionalCase
    {


        [Test]
        public void OverlapedShipPlacing_OK()
        {
            Mock<IBattleshipsConsoleGameMessages> battleshipsMessages = new Mock<IBattleshipsConsoleGameMessages>();
            Mock<IConsoleWraper> consoleWrapperMock = new Mock<IConsoleWraper>();
            battleshipsMessages.SetupGet(x => x.YesAnswer).Returns('Y');
            battleshipsMessages.SetupGet(x => x.NoAnswer).Returns('N');

            var step = -1;
            var steps = new List<string> {
                "A", "1", "Y",
                "A", "1", "Y",
                "C", "3", "N",
                "C", "3", "N",
                "D", "6", "N",
                "J", "4", "Y",
                "E", "7", "Y",
            };

            steps.AddRange(Steps.CorrectSuccessiveSteps);


            consoleWrapperMock.Setup(x => x.ReadLine()).Returns(() =>
            {
                step++;
                return steps[step];
            });

            new BattleshipsConsoleGame(battleshipsMessages.Object, consoleWrapperMock.Object).Start();

            //consoleWrapperMock.Verify(x => x.WriteLine(Mock.Of<string>()));
        }

        [Test]
        public void SameMoveTwice_OK()
        {
            Mock<IConsoleWraper> consoleWrapperMock = new Mock<IConsoleWraper>();
            Mock<IBattleshipsConsoleGameMessages> battleshipsMessages = new Mock<IBattleshipsConsoleGameMessages>();

            battleshipsMessages.SetupGet(x => x.YesAnswer).Returns('Y');
            battleshipsMessages.SetupGet(x => x.NoAnswer).Returns('N');

            var step = -1;
            var steps = Steps.CorrectShipPlaicing;
            steps.AddRange(new List<string> { 
                "A", "1",
                "A", "1"
            });
            steps.AddRange(Steps.CorrectSuccessiveSteps);
            steps.AddRange(new List<string> { "N" });


            consoleWrapperMock.Setup(x => x.ReadLine()).Returns(() =>
            {
                step++;
                return steps[step];
            });

            new BattleshipsConsoleGame(battleshipsMessages.Object, consoleWrapperMock.Object).Start();
        }


    }
}
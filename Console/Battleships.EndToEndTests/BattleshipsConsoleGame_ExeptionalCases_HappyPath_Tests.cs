using NUnit.Framework;
using Battleships.ConsoleWrapper;
using Moq;
using System;
using System.Collections.Generic;
using Battleships.Core;

namespace Battleships.EndToEndTests
{
    public class BattleshipsConsoleGame_ExeptionalCases_HappyPath_Tests
    {
        const string gameOverMessage = "GameOverMessage";
        const string someoneWonMessage = "Someone Won";

        Mock<IBattleshipsConsoleGameMessages> _battleshipsMessages;
        Mock<IConsoleWraper> _consoleWrapperMock;
        
        [SetUp]
        public void SetUp()
        {
            _battleshipsMessages = new Mock<IBattleshipsConsoleGameMessages>();
            _consoleWrapperMock = new Mock<IConsoleWraper>();
            _battleshipsMessages.SetupGet(x => x.YesAnswer).Returns('Y');
            _battleshipsMessages.SetupGet(x => x.NoAnswer).Returns('N');

            _battleshipsMessages.SetupGet(x => x.GameOver).Returns(gameOverMessage);
            _battleshipsMessages.Setup(x => x.GetPlayerWinMessage(It.IsAny<Player>())).Returns(someoneWonMessage);
        }

        [Test]
        public void HappyPath_OneGame_ShouldOutputGameOver()
        {
            // Arrange

            var step = -1;
            var steps = new List<string>( Steps.CorrectShipPlaicing );
            steps.AddRange(Steps.CorrectSuccessiveSteps);
            steps.AddRange(Steps.NoNewGame);

            _consoleWrapperMock.Setup(x => x.ReadLine()).Returns(() =>
            {
                step++;
                return steps[step];
            });

            
            var game = new BattleshipsConsoleGame(_battleshipsMessages.Object, _consoleWrapperMock.Object);

            // Act
            game.Start();

            // Assert
            AssertGameHasFinishedCorrectly(Times.Once());
        }

        [Test]
        public void HappyPath_ThreeGames_ShouldOutputGameOver()
        {
            // Arrange

            var step = -1;
            var steps = new List<string>(Steps.CorrectShipPlaicing);
            steps.AddRange(Steps.CorrectSuccessiveSteps);
            steps.AddRange(Steps.NewGame);

            steps.AddRange(Steps.CorrectShipPlaicing);
            steps.AddRange(Steps.CorrectSuccessiveSteps);
            steps.AddRange(Steps.NewGame);

            steps.AddRange(Steps.CorrectShipPlaicing);
            steps.AddRange(Steps.CorrectSuccessiveSteps);
            steps.AddRange(Steps.NoNewGame);

            _consoleWrapperMock.Setup(x => x.ReadLine()).Returns(() =>
            {
                step++;
                return steps[step];
            });


            var game = new BattleshipsConsoleGame(_battleshipsMessages.Object, _consoleWrapperMock.Object);

            // Act
            game.Start();

            // Assert
            AssertGameHasFinishedCorrectly(Times.Exactly(3));
        }

        public void AssertGameHasFinishedCorrectly(Moq.Times times)
        {
            _consoleWrapperMock.Verify(m => m.WriteLine(It.Is<string>(input => input == gameOverMessage)), times);
            _consoleWrapperMock.Verify(m => m.WriteLine(It.Is<string>(input => input == someoneWonMessage)), times);
        }
    }
}
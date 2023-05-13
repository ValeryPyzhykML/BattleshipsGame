using NUnit.Framework;

namespace Battleships.ConsoleWrapper.Tests
{
    [TestFixture]
    public class BattleshipsConsoleGameTests
    {
        private Mock<IConsoleWraper> _consoleMock;
        private Mock<IBattleshipsConsoleGameMessages> _messagesMock;
        private BattleshipsConsoleGame _game;

        [SetUp]
        public void SetUp()
        {
            _consoleMock = new Mock<IConsoleWraper>();
            _messagesMock = new Mock<IBattleshipsConsoleGameMessages>();
            _game = new BattleshipsConsoleGame(_messagesMock.Object, _consoleMock.Object);
        }

        [Test]
        public void Start_WhenGameIsOver_ShouldNotRestart()
        {
            // Arrange
            var gameMock = new Mock<BattleshipsGame>();
            gameMock.SetupGet(g => g.IsOver).Returns(true);
            _messagesMock.Setup(m => m.WantToRestartMessage).Returns("Do you want to restart?");
            _consoleMock.Setup(c => c.ReadLine()).Returns("N");

            // Act
            _game.Start();

            // Assert
            _messagesMock.Verify(m => m.WantToRestartMessage, Times.Never);
            _consoleMock.Verify(c => c.ReadLine(), Times.Never);
        }

        [Test]
        public void Start_WhenGameIsNotOver_ShouldRestart()
        {
            // Arrange
            var gameMock = new Mock<BattleshipsGame>();
            gameMock.SetupSequence(g => g.IsOver).Returns(false).Returns(true);
            _messagesMock.Setup(m => m.WantToRestartMessage).Returns("Do you want to restart?");
            _consoleMock.SetupSequence(c => c.ReadLine()).Returns("Y").Returns("N");

            // Act
            _game.Start();

            // Assert
            _messagesMock.Verify(m => m.WantToRestartMessage, Times.Exactly(2));
            _consoleMock.Verify(c => c.ReadLine(), Times.Exactly(2));
        }

        [Test]
        public void Start_WhenExceptionOccurs_ShouldPrintErrorMessage()
        {
            // Arrange
            _consoleMock.Setup(c => c.WriteLine(It.IsAny<string>()));
            _consoleMock.Setup(c => c.ReadLine()).Throws(new SomeException());

            // Act
            _game.Start();

            // Assert
            _consoleMock.Verify(c => c.WriteLine(It.IsAny<string>()), Times.Once);
        }

        // Add more unit tests for other methods as needed
    }
}

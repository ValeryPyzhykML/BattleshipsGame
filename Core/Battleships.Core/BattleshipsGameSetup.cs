using Battleships.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Batteships.Core.Tests")]
namespace Battleships.Core
{
    public class BattleshipsGameSetup
    {
        public List<ShipClass> ShipsToAdd;
        public IPlayerBoard PlayerBoard => _firstPlayerBoard;
        public IOpponentBoard OpponentBoard => _secondPlayerBoard;

        private readonly List<ShipClass> _secondPlayerShipsToAdd;
        private readonly Board _firstPlayerBoard;
        private readonly Board _secondPlayerBoard;
        private readonly AIPlayer _AIPlayer;
        private bool _isGameStarted = false;
        

        public BattleshipsGameSetup() {
            ShipsToAdd = new List<ShipClass>() { 
                ShipClass.Carrier, 
                ShipClass.Battleship, 
                ShipClass.Cruiser, 
                ShipClass.Destroyer, 
                ShipClass.Submarine 
            };
            _secondPlayerShipsToAdd = new List<ShipClass>(ShipsToAdd);
            _firstPlayerBoard = new Board();
            _secondPlayerBoard = new Board();
            _AIPlayer = new AIPlayer();
        }

        public bool TryPlaceShip(char column, int row, bool isVertical, ShipClass shipClass)
        {
            var ship = new Ship(shipClass);
            return _firstPlayerBoard.TryPlaceShip(column, row, isVertical, ship);
        }

        private void LetOpponentPlaceShips()
        {
            foreach (var shipClass in _secondPlayerShipsToAdd)
            {
                _AIPlayer.PlaceSheep(shipClass, _secondPlayerBoard);
            }
        }

        public BattleshipsGame StartGame()
        {
            if (_isGameStarted)
            {
                throw new GameAlreadyStartedException();
            }
            if (!_firstPlayerBoard.IsReadyDoStart()) 
            {
                throw new ApplicationException("The player board is not ready to start."); // TODO
            }
            LetOpponentPlaceShips();
            if (!_secondPlayerBoard.IsReadyDoStart())
            {
                throw new ApplicationException("The AI board is not ready to start."); // TODO
            }
            return new BattleshipsGame(_firstPlayerBoard, _secondPlayerBoard, _AIPlayer);
        }
    }
}

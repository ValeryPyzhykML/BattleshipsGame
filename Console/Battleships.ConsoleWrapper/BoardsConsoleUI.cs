using Battleships.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.ConsoleWrapper
{
    internal class BoardsConsoleUI : IBoardsConsoleUI
    {
        private const char UndescoveredCellSymbol = '.';
        private const char MissCellSymbol = 'O';
        private const char HitCellSymbol = 'X';

        private IConsoleWraper _console;
        private IBattleshipsConsoleGameMessages _messages;

        internal BoardsConsoleUI(IBattleshipsConsoleGameMessages messages, IConsoleWraper console)
        {
            _console = console;
            _messages = messages;
        }

        void IBoardsConsoleUI.ShowBoards(IPlayerBoard playerBoard, IOpponentBoard opponentBoard)
        {
            _console.Write("    ");
            for (var column = 0; column < BattleshipsGameConstans.BoardSideSize; column++)
            {
                _console.Write($" {(char)(BattleshipsGameConstans.FirstColumnLetter + column)} ");
            }
            _console.Write("   ");
            for (var column = 0; column < BattleshipsGameConstans.BoardSideSize; column++)
            {
                _console.Write($" {(char)(BattleshipsGameConstans.FirstColumnLetter + column)} ");
            }
            _console.WriteLine("   ");
            for (var row = BattleshipsGameConstans.BoardFirstRowNumber; row <= BattleshipsGameConstans.BoardLastRowNumber; row++)
            {
                var extraSpace = row < 10 ? " " : "";
                _console.Write($" {extraSpace}{row} ");
                for (var column = BattleshipsGameConstans.FirstColumnLetter; column <= BattleshipsGameConstans.LastColumnLetter; column++)
                {
                    _console.Write($" {ShowPlayerCell(playerBoard, column, row)} ");
                }
                _console.Write($"   ");
                for (var column = BattleshipsGameConstans.FirstColumnLetter; column <= BattleshipsGameConstans.LastColumnLetter; column++)
                {
                    _console.Write($" {ShowOponentCell(opponentBoard, column, row)} ");
                }
                _console.WriteLine();
            }
        }

        private char ShowPlayerCell(IPlayerBoard board, char column, int row)
        {
            var shipClass = board.GetShipClass(column, row);
            if (shipClass != null)
            {
                return _messages.GetShipLetter(shipClass.Value);
            }
            if (board.GetStatus(column, row) == CellStatus.Undescovered)
            {
                return UndescoveredCellSymbol;
            }
            if (board.GetStatus(column, row) == CellStatus.Miss)
            {
                return MissCellSymbol;
            }
            return HitCellSymbol;
        }

        private char ShowOponentCell(IOpponentBoard board, char column, int row)
        {
            if (board.GetStatus(column, row) == CellStatus.Undescovered)
            {
                return UndescoveredCellSymbol;
            }
            if (board.GetStatus(column, row) == CellStatus.Miss)
            {
                return MissCellSymbol;
            }
            return _messages.GetShipLetter(board.GetShipClass(column, row).Value);
        }
    }
}

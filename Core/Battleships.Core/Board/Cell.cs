using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Core
{
    public class Cell
    {
        private static Cell _missCell = new Cell() { Status = CellStatus.Miss };
        internal static Cell MissCell => _missCell;
        public Ship Ship { get; internal set; }
        public CellStatus Status { get; internal set; }
    }
}

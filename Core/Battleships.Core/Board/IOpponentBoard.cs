﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Core
{
    public interface IOpponentBoard 
    {
        public CellStatus GetStatus(char column, int row);
        public ShipClass? GetShipClass(char column, int row);
        public Ship MakeMove(char column, int row);
        public int LifesLeft { get; }
    }
}

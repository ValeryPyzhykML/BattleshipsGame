﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Core.Exceptions
{
    public class BattleshipsGameException : ApplicationException
    {
        public BattleshipsGameException(string message) : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using Battleships.ConsoleWrapper;
using Battleships.Core;

namespace Battleships.ConsoleUI
{
    class Program
    {
        public static void Main()
        {    
            new BattleshipsConsoleGame().Start();
            Console.ReadLine();
        }
        
    }
}

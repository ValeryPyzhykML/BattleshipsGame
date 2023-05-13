using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Core.Exceptions
{
    public class ShipClassAlreadyPresentedException : BattleshipsGameException
    {
        public ShipClassAlreadyPresentedException() : base("Cannot the ship with this class for it's already added.")
        {
        }
    }
}

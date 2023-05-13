using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Core.Exceptions
{
    public class MoveAfterGameOverException : BattleshipsGameException
    {
        public MoveAfterGameOverException() : base("Cannot make a move after game over.")
        {
        }
    }
}

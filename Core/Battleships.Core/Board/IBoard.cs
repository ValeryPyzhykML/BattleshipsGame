using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Core
{
    internal interface IBoard : IPlayerBoard, IOpponentBoard
    {
    }
}

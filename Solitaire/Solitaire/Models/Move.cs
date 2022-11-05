using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Models
{
    public class Move
    {
        public Position FromPosition { get; }

        public Position ToPosition { get; }

        public Marble RemovedMarble { get; }

        public Move(Position fromPosition, Position toPosition, Marble removedPosition)
        {
            FromPosition = fromPosition;
            ToPosition = toPosition;
            RemovedMarble = removedPosition;
        }
    }
}

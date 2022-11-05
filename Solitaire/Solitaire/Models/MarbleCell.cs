using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Models
{
    public class MarbleCell
    {
        public Position Position { get; }
        public bool IsSelected { get; set; }

        public bool IsEmpty { get; set; }

        public MarbleCell(int x, int y, bool isEmpty)
        {
            Position = new Position(x, y);
            IsEmpty = isEmpty;
            IsSelected = false;

        }
    }
}

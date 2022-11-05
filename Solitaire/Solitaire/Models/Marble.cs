using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Models
{
   public class Marble
    {
        public Position Position { get; set; }
        public bool IsSelected { get; set; }

        public Marble(int x, int y)
        {
            Position = new Position(x, y);
            IsSelected = false;
        }
    }
}

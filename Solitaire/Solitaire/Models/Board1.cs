using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Models
{
    public class Board1
    {
        private const int BoardWidth = 7;

        private const int BoardHeight = 7;
        public List<Marble> Marbles { get; private set; } = new List<Marble>();
        public int PegCounter { get; private set; } = 32;

        public Board1()
        {
            InitialState();
        }

        public void InitialState()
        {
            Marbles.Clear();

            for (int i = 0; i < BoardWidth; i++)
            {
                for (int j = 0; j < BoardHeight; j++)
                {
                    if ((i < 2 && j < 2) ||
                        (i < 2 && j > 4) ||
                        (i > 4 && j < 2) ||
                        (i > 4 && j > 4) ||
                        (i == 3 && j == 3)) continue;

                    Marbles.Add(new Marble(i, j));
                }
            }

            PegCounter = 32;
        }
    }
}

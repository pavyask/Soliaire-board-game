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
        public List<MarbleCell> MarbleCells { get; private set; } = new List<MarbleCell>();
        public int MarbleCounter { get; set; } = 32;

        public Board1()
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    if ((x < 2 && y < 2) ||
                        (x < 2 && y > 4) ||
                        (x > 4 && y < 2) ||
                        (x > 4 && y > 4))
                        continue;
                    else if (x == 3 && y == 3)
                        MarbleCells.Add(new MarbleCell(x, y, true));
                    else
                        MarbleCells.Add(new MarbleCell(x, y, false));
                }
            }
        }

        public void ResetBoard()
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    if ((x < 2 && y < 2) ||
                        (x < 2 && y > 4) ||
                        (x > 4 && y < 2) ||
                        (x > 4 && y > 4))
                        continue;
                    else if (x == 3 && y == 3)
                        MarbleCells.Find(cell => cell.Position.X == x && cell.Position.Y == y).IsEmpty = true;
                    else
                        MarbleCells.Find(cell => cell.Position.X == x && cell.Position.Y == y).IsEmpty = false;
                }
            }
            var test = MarbleCells.Find(cell => cell.Position.X == 3 && cell.Position.Y == 3);

            MarbleCounter = 32;
        }
    }
}

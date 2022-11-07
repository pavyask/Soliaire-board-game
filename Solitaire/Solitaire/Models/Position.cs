namespace Solitaire.Models
{
    public class Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position(Position position)
        {
            X = position.X;
            Y = position.Y;
        }

        public override string ToString()
        {
            return $"X={X},Y={Y}";
        }
    }
}

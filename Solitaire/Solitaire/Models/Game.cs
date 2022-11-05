using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Solitaire.Models
{
    public class Game
    {
        public Board1? Board { get; private set; }

        public List<Move> Moves { get; private set; } = new List<Move>();

        public Marble? CurrentMarble { get; private set; }

        public Game(Board1 board1)
        {
            Board = board1;
        }

        public void MoveAttempt(Position pos)
        {
            MessageBox.Show(pos.ToString());
            var x = pos.X;
            var y = pos.Y;

            if (IsCellEmpty(x, y) && CurrentMarble == null)
                return;

            else if (!IsCellEmpty(x, y))
            {
                DeselectCurrentMarble();
                CurrentMarble = GetMarbleOnPos(x, y);
                CurrentMarble.IsSelected = true;
            }

            else if (IsCellEmpty(x, y) && CurrentMarble != null)
            {
                if (IsMoveValid(CurrentMarble, x, y))
                {
                    MoveMarble(x, y);
                    DeselectCurrentMarble();
                }
            }

            if (CheckWin())
                MessageBox.Show("You WIN!", "Win Message");

            if (CheckLose())
                MessageBox.Show("You LOST!", "Lose Message");
        }

        public bool CheckWin() => Board.PegCounter == 1;

        public bool CheckLose()
        {
            return false;
        }

        public bool IsMoveValid(Marble marble, int destX, int destY)
        {
            if (marble == null)
                return false;

            var midX = (marble.Position.X + destX) / 2;
            var midY = (marble.Position.X + destY) / 2;

            if (((Math.Abs(marble.Position.Y - destY) == 2 &&
                  marble.Position.X == destX)
                 ||
                 (Math.Abs(marble.Position.X - destX) == 2 &&
                  marble.Position.Y == destY))
                &&
                (!IsCellEmpty(midX, midY)))
                return true;

            else return false;
        }

        public void MoveMarble(int destX, int destY)
        {
            if (CurrentMarble == null)
                return;

            var midX = (CurrentMarble.Position.X + destX) / 2;
            var midY = (CurrentMarble.Position.Y + destY) / 2;

            var fromPosition = new Position(CurrentMarble.Position);
            var toPosition = new Position(destX, destY);
            var removedMarble = RemoveMarbleOnPos(midX, midY);
            Moves.Add(new Move(fromPosition, toPosition, removedMarble));

            CurrentMarble.Position.X = destX;
            CurrentMarble.Position.Y = destY;
            DeselectCurrentMarble();
        }

        public void MoveBack()
        {
            MessageBox.Show("MoveBack");
            if (Moves.Count == 0)
                return;

            var lastMove = Moves.Last();
            var marble = GetMarbleOnPos(lastMove.ToPosition);
            var removedMarble = lastMove.RemovedMarble;

            marble.Position.X = lastMove.FromPosition.X;
            marble.Position.Y = lastMove.FromPosition.Y;
            Board.Marbles.Add(removedMarble);
            Moves.Remove(lastMove);
            DeselectCurrentMarble();
        }

        public Marble? GetMarbleOnPos(int x, int y) =>
            Board.Marbles.Find(marble => marble.Position.X == x && marble.Position.Y == y);

        public Marble? GetMarbleOnPos(Position position) => Board.Marbles.Find(marble =>
            marble.Position.X == position.X && marble.Position.Y == position.Y);

        public Marble? RemoveMarbleOnPos(int x, int y)
        {
            var marble = GetMarbleOnPos(x, y);
            if (marble == null)
                return null;
            else
            {
                Board.Marbles.Remove(marble);
                return marble;
            }
        }

        public bool IsCellEmpty(int x, int y) => GetMarbleOnPos(x, y) == null;

        public void DeselectCurrentMarble()
        {
            if (CurrentMarble != null)
            {
                CurrentMarble.IsSelected = false;
                CurrentMarble = null;
            }
        }

        public void Restart()
        {
            MessageBox.Show("Restart");
            DeselectCurrentMarble();
            Moves.Clear();
            Board.InitialState();
        }
    }
}

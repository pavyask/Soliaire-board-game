using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Solitaire.Models
{
    public class Game
    {
        public Board1? Board { get; private set; }

        public List<Move> Moves { get; private set; } = new List<Move>();

        public MarbleCell? CurrentMarble { get; private set; }

        public Game(Board1 board1)
        {
            Board = board1;
        }

        public void MoveAttempt(Position pos)
        {
            var marbleCell = GetMarbleCellOnPos(pos);

            if (marbleCell.IsEmpty && CurrentMarble == null)
                return;

            else if (!marbleCell.IsEmpty)
            {
                DeselectCurrentMarble();
                CurrentMarble = marbleCell;
                CurrentMarble.IsSelected = true;
            }

            else if (marbleCell.IsEmpty && CurrentMarble != null)
            {
                if (IsMoveValid(CurrentMarble, marbleCell))
                {
                    MoveMarble(marbleCell);
                    DeselectCurrentMarble();
                }
            }

            if (CheckWin())
                MessageBox.Show("You WIN!", "Win Message");

            if (CheckLose())
                MessageBox.Show("You LOST!", "Lose Message");
        }

        public bool CheckWin() => Board.MarbleCounter == 1;

        public bool CheckLose()
        {
            foreach (var cell in Board.MarbleCells)
            {
                if (ExistValidMove(cell)) return false;
            }
            return true;
        }

        public bool ExistValidMove(MarbleCell cell)
        {
            if (cell.IsEmpty == true)
                return false;

            var x = cell.Position.X;
            var y = cell.Position.Y;

            List<MarbleCell?> cellsToCheck = new List<MarbleCell?>
            {
                GetMarbleCellOnPos(x, y - 2),
                GetMarbleCellOnPos(x, y + 2),
                GetMarbleCellOnPos(x - 2, y),
                GetMarbleCellOnPos(x + 2, y)
            };

            foreach (var cellToCheck in cellsToCheck)
            {
                if (IsMoveValid(cell, cellToCheck)) return true;
            }

            return false;
        }

        public bool IsMoveValid(MarbleCell? marble, MarbleCell? destCell)
        {
            if (marble == null || destCell == null ||
                marble.IsEmpty || !destCell.IsEmpty)
                return false;

            var destX = destCell.Position.X;
            var destY = destCell.Position.Y;

            var midX = (marble.Position.X + destX) / 2;
            var midY = (marble.Position.Y + destY) / 2;

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

        public void MoveMarble(MarbleCell? destCell)
        {
            if (CurrentMarble == null || destCell == null ||
                CurrentMarble.IsEmpty || !destCell.IsEmpty)
                return;

            var destX = destCell.Position.X;
            var destY = destCell.Position.Y;
            var midX = (CurrentMarble.Position.X + destX) / 2;
            var midY = (CurrentMarble.Position.Y + destY) / 2;

            var removedMarble = RemoveMarbleOnPos(midX, midY);
            removedMarble.IsEmpty = true;
            Moves.Add(new Move(CurrentMarble, destCell, removedMarble));

            CurrentMarble.IsEmpty = true;
            destCell.IsEmpty = false;
            DeselectCurrentMarble();
        }

        public void MoveBack()
        {
            if (Moves.Count == 0)
                return;

            var lastMove = Moves.Last();
            lastMove.ToCell.IsEmpty = true;
            lastMove.RemovedMarble.IsEmpty = false;
            lastMove.FromCell.IsEmpty = false;
            Board.MarbleCounter++;
            Moves.Remove(lastMove);
            DeselectCurrentMarble();
        }

        public MarbleCell? GetMarbleCellOnPos(int x, int y) =>
            Board.MarbleCells.Find(cell => cell.Position.X == x && cell.Position.Y == y);

        public MarbleCell? GetMarbleCellOnPos(Position position) => Board.MarbleCells.Find(cell =>
            cell.Position.X == position.X && cell.Position.Y == position.Y);

        public MarbleCell? RemoveMarbleOnPos(int x, int y)
        {
            var marbelCell = GetMarbleCellOnPos(x, y);
            if (marbelCell == null || marbelCell.IsEmpty)
                return null;
            else
            {
                marbelCell.IsEmpty = false;
                Board.MarbleCounter--;
                return marbelCell;
            }
        }

        public bool IsCellEmpty(int x, int y) => GetMarbleCellOnPos(x, y).IsEmpty;

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
            DeselectCurrentMarble();
            Moves.Clear();
            Board.ResetBoard();
        }
    }
}

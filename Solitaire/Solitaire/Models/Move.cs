namespace Solitaire.Models
{
    public class Move
    {
        public MarbleCell FromCell { get; }

        public MarbleCell ToCell { get; }

        public MarbleCell RemovedMarble { get; }

        public Move(MarbleCell fromCell, MarbleCell toCell, MarbleCell removedMarble)
        {
            FromCell = fromCell;
            ToCell = toCell;
            RemovedMarble = removedMarble;
        }
    }
}

using System.Windows.Controls;
using System.Windows.Media;

namespace Solitaire.Models
{
    public class MarbleCell:Button
    {
        public Position Position { get; }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == false)
                    Background = _isEmpty ? Brushes.Gray : Brushes.Blue;
                else
                    Background = Brushes.Green;
                _isSelected = value;
            }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                if (value == false)
                    Background = Brushes.Blue;
                else
                    Background = Brushes.Gray;
                _isEmpty = value;
            }
        }

        public MarbleCell(int x, int y, bool isEmpty)
        {
            Position = new Position(x, y);
            IsEmpty = isEmpty;
            IsSelected = false;

        }

    }
}

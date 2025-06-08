using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace SortWaterPuzzle_console.Models
{
    public class Tube : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private const int Capacity = 4;
        private readonly Stack<WaterColor> _contents = new();

        public bool IsEmpty => !_contents.Any();
        public bool IsFull => _contents.Count == Capacity;
        
        public WaterColor Top => _contents.Count > 0 ? _contents.Peek() : WaterColor.None;
        public int AvailableSpace => Capacity - _contents.Count;
        
        private IReadOnlyCollection<WaterColor> _contentsView;
        public IReadOnlyCollection<WaterColor> Contents
        {
            get => _contentsView;
            private set
            {
                _contentsView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contents)));
            }
        }
        
        public Tube()
        {
            UpdateContents();
        }

        private void UpdateContents()
        {
            Contents = _contents.ToList();
        }
        
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
            }
        }


        public bool CanPourInto(Tube target)
        {
            if (this.IsEmpty) return false;
            if (target.IsFull) return false;
            return target.IsEmpty || target.Top == this.Top;
        }

        public void PourInto(Tube target)
        {
            if (!CanPourInto(target)) return;

            var movingColor = this.Top;
            int moveCount = 0;

            while (this.Top == movingColor && !target.IsFull)
            {
                target._contents.Push(this._contents.Pop());
                moveCount++;
            }

            UpdateContents();
            target.UpdateContents();
        }

        public bool IsSorted()
        {
            return _contents.Count == 0 || (_contents.Count == Capacity && _contents.All(c => c == _contents.Peek()));
        }

        // public void Fill(IEnumerable<WaterColor> colors)
        // {
        //     _contents.Clear();
        //     foreach (var color in colors.Reverse())
        //         _contents.Push(color);
        //     
        //     UpdateContents();
        // }
        
        public void Fill(IEnumerable<WaterColor> colors)
        {
            _contents.Clear();
            foreach (var color in colors)
                _contents.Push(color);
    
            UpdateContents();
        }
    }
}
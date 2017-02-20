using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinLifeGameXAML
{
    public class LgViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Cell[] _cells;

        public LgViewModel()
        {
            var cells = new Cell[9 * 9];

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    var index = (j + i * 8);
                    cells[index] = new Cell {
                        State = 0,
                        IndexX = i,
                        IndexY = j,
                        Index =  index,
                        Text = index.ToString()
                    };
                }
            }

            Cells = cells;
        }

        public Cell[] Cells
        {
            get
            {
                var cells = new Cell[9];
                foreach (var cell in _cells)
                {
                    if (cell.IndexX == 0)
                    {
                        cells[cell.IndexY] = cell;
                    }
                }

                return cells;
            }
            private set { _cells = value; }
        }
    }
}

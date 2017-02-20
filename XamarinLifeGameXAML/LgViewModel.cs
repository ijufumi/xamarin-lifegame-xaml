using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinLifeGameXAML
{
    public class LgViewModel : INotifyPropertyChanged
    {
        private const int CellSize = 9;

        public event PropertyChangedEventHandler PropertyChanged;

        public LgViewModel()
        {
            var cells = new Cell[CellSize * CellSize];

            for (var i = 0; i < CellSize; i++)
            {
                for (var j = 0; j < CellSize; j++)
                {
                    var index = (j + i * CellSize);
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

        public Cell[] Cells0 => GetCells(0);
        public Cell[] Cells1 => GetCells(1);
        public Cell[] Cells2 => GetCells(2);
        public Cell[] Cells3 => GetCells(3);
        public Cell[] Cells4 => GetCells(4);
        public Cell[] Cells5 => GetCells(5);
        public Cell[] Cells6 => GetCells(6);
        public Cell[] Cells7 => GetCells(7);
        public Cell[] Cells8 => GetCells(8);

        private Cell[] GetCells(int colIndex)
        {
            var cells = new Cell[CellSize];
            foreach (var cell in Cells)
            {
                if (cell.IndexX == colIndex)
                {
                    cells[cell.IndexY] = cell;
                }
            }

            return cells;
        }

        public Cell[] Cells
        {
            get;
            private set;
        }
    }
}

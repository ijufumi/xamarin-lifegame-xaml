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

        public Cell[] Cells
        {
            get;
            private set;
        }
    }
}

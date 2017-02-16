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
            get;
            private set;
        }
    }
}

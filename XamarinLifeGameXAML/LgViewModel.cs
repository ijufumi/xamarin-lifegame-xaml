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

        ICommand command;

        public LgViewModel()
        {
            command = new Command(CellTapped);
            var cells = new Cell[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++) {
                    cells[i, j] = new Cell { 
                        State = 0,
                        IndexX = i,
                        IndexY = j,
                        Text = (i + j * 8).ToString()
                    };
                }
            }

            Cells = cells;
        }

        public Cell[,] Cells
        {
            get;
            private set;
        }

        public ICommand Tapped() => command;

        void CellTapped(object sender)
        {
            Debug.WriteLine(sender);
        }
    }
}

using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using Prism.Commands;
using Prism.Mvvm;
using Xamarin.Forms;
using XamarinLifeGameXAML.Logic;
using Cell = XamarinLifeGameXAML.Logic.Cell;

namespace XamarinLifeGameXAML.ViewModels
{
    public class LifeGameViewModel : BindableBase
    {

        public LifeGameViewModel()
        {
            this.StartCommand = new DelegateCommand(
                    async () => await ControlGame(),
                    () => !IsExecuted
            )
            .ObservesProperty(() => IsExecuted);

            this.StopCommand = new DelegateCommand(
                    async () => await ControlGame(),
                    () => IsExecuted
                )
                .ObservesProperty(() => IsExecuted);


        }

        private bool isExecuted;
        /*
         * DelegateCommandでは、プロパティじゃないとエラーで落ちるので、
         * 単純なものでも要プロパティ。
         */
        public bool IsExecuted
        {
            get { return this.isExecuted; }
            set { SetProperty(ref this.isExecuted, value); }
        }

        public DelegateCommand StartCommand { get; }

        public DelegateCommand StopCommand { get; }

        public Cell[] Cells { get; set; }

        private async Task ControlGame()
        {
            Debug.WriteLine("Call ControlGame : " + IsExecuted);
            if (IsExecuted)
            {
                IsExecuted = false;
                await Task.Run(UpdateCellsAsync);
            }
            else
            {
                IsExecuted = true;
            }
        }

        private async Task StartGame()
        {
            await UpdateCellsAsync();
        }

        private async Task UpdateCellsAsync()
        {
            var tcs = new TaskCompletionSource<object>();
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    UpdateCells();
                    tcs.SetResult(null);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
        }

        private void UpdateCells()
        {
            var temp = Cells;
            for (var i = 0; i < CellUtils.CellSize; i++)
            {
                for (var j = 0; j < CellUtils.CellSize; j++)
                {
                    temp[CellUtils.GetIndex(i, j)].State = JudgeNextLife(i, j);
                }
            }

            Cells = temp;
        }

        private int JudgeNextLife(int x, int y)
        {
            var neighborsCount = 0;

            if (x > 0)
            {
                if (Cells[CellUtils.GetIndex(x - 1, y)].State == 1)
                {
                    neighborsCount++;
                }
                if (y > 0 && Cells[CellUtils.GetIndex(x - 1, y - 1)].State == 1)
                {
                    neighborsCount++;
                }
                if (y < CellUtils.CellSize - 1 && Cells[CellUtils.GetIndex(x - 1, y + 1)].State == 1)
                {
                    neighborsCount++;
                }
            }
            if (x < CellUtils.CellSize - 1)
            {
                if (Cells[CellUtils.GetIndex(x + 1, y)].State == 1)
                {
                    neighborsCount++;
                }
                if (y > 0 && Cells[CellUtils.GetIndex(x + 1, y - 1)].State == 1)
                {
                    neighborsCount++;
                }
                if (y < CellUtils.CellSize - 1 && Cells[CellUtils.GetIndex(x + 1, y + 1)].State == 1)
                {
                    neighborsCount++;
                }
            }
            if (y > 0)
            {
                if (Cells[CellUtils.GetIndex(x, y - 1)].State == 1)
                {
                    neighborsCount++;
                }
            }
            if (y < CellUtils.CellSize - 1)
            {
                if (Cells[CellUtils.GetIndex(x, y + 1)].State == 1)
                {
                    neighborsCount++;
                }
            }
            var life = Cells[CellUtils.GetIndex(x, y)].State;
            var newLife = 0;

            if (life == 1)
            {
                if (neighborsCount == 3 || neighborsCount == 2)
                {
                    newLife = 1;
                }
            }
            else
            {
                if (neighborsCount == 3)
                {
                    newLife = 1;
                }
            }

            Debug.WriteLine("Cell : x[" + x + "] y[" + y + "] Neighbor[" + neighborsCount + "] Life[" + newLife + "]");

            return newLife;
        }
    }
}

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
            StartCommand = new DelegateCommand(
                    async () => await ControlGame(),
                    () => !IsExecuted
            )
            .ObservesProperty(() => IsExecuted);

            StopCommand = new DelegateCommand(
                    async () => await ControlGame(),
                    () => IsExecuted
                )
                .ObservesProperty(() => IsExecuted);


        }

        private bool isExecuted;

        // DelegateCommandでは、プロパティじゃないとエラーで落ちるので、
        // 単純なものでも要プロパティ。
        public bool IsExecuted
        {
            get { return isExecuted; }
            set { SetProperty(ref isExecuted, value); }
        }

        public DelegateCommand StartCommand { get; }

        public DelegateCommand StopCommand { get; }

        public Cell[] Cells { get; set; }

        // START/STOPボタンから実行されるメソッド。
        private async Task ControlGame()
        {
            Debug.WriteLine("Call ControlGame : " + IsExecuted);
            if (!IsExecuted)
            {
                IsExecuted = true;
                // awaitすると処理を待つので、asyncで処理を実行する
                await Task.Run(async () =>
                {
                    while (IsExecuted)
                    {
                        UpdateCells();
                        await Task.Delay(100);
                    }
                });
            }
            else
            {
                IsExecuted = false;
            }
        }

        // Cellの更新を行う
        private void UpdateCells()
        {
            // UIスレッドからじゃないと、UIの更新ができないので
            Device.BeginInvokeOnMainThread(() =>
            {
                var temp = (Cell[]) Cells.Clone();
                for (var i = 0; i < CellUtils.CellSize; i++)
                {
                    for (var j = 0; j < CellUtils.CellSize; j++)
                    {
                        temp[CellUtils.GetIndex(i, j)].State = JudgeNextLife(i, j);
                    }
                }

                Cells = temp;
            });
        }

        // ライフゲームのメイン処理
        // 現世で生きていて且つ、周りの人が3 or 2なら、来世は生きる
        // 現世で死んでいて且つ、周りの人が3なら、来世は生きる
        // それ以外は死
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

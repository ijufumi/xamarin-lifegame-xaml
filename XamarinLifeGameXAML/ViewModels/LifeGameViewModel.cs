using System;
using System.Threading.Tasks;
using System.Diagnostics;
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
            StartCommand = new DelegateCommand<string>(
                    async T => await ControlGame(T),
                    T => !IsExecuted
            )
            .ObservesProperty(() => IsExecuted);

            StopCommand = new DelegateCommand<string>(
                    async T => await ControlGame(T),
                    T => IsExecuted
                )
                .ObservesProperty(() => IsExecuted);

            CellClick = new DelegateCommand<int?>(
                    async T => await CellClicked(T),
                    T => !IsExecuted)
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

        public DelegateCommand<string> StartCommand { get; }

        public DelegateCommand<string> StopCommand { get; }

        public DelegateCommand<int?> CellClick { get; }

        public Cell[] Cells { get; set; }

        public async Task CellClicked(int? parameter)
        {
            if (parameter.HasValue)
            {
                var index = parameter.Value;
                Cells[index].ChangeState();
                Debug.WriteLine("CellClicked [" + parameter + "], IsLive[" + Cells[index].IsLive + "]");
            }
        }

        // START/STOPボタンから実行されるメソッド。
        private async Task ControlGame(string buttonType)
        {
            Debug.WriteLine("Call ControlGame : " + buttonType);

            // TODO:
            if (buttonType.Equals("start"))
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
                var nextStep = (Cell[]) Cells.Clone();
                for (var i = 0; i < CellUtils.CellSize; i++)
                {
                    for (var j = 0; j < CellUtils.CellSize; j++)
                    {
                        if (JudgeNextLife(i, j))
                        {
                            nextStep[CellUtils.GetIndex(i, j)].ToLive();
                        }
                        else
                        {
                            nextStep[CellUtils.GetIndex(i, j)].ToDead();
                        }
                    }
                }

                Cells = nextStep;
            });
        }

        // ライフゲームのメイン処理
        // 現世で生きていて且つ、周りの人が3 or 2なら、来世は生きる
        // 現世で死んでいて且つ、周りの人が3なら、来世は生きる
        // それ以外は死
        private bool JudgeNextLife(int x, int y)
        {
            var neighborsCount = 0;

            if (x > 0)
            {
                if (Cells[CellUtils.GetIndex(x - 1, y)].IsLive)
                {
                    neighborsCount++;
                }
                if (y > 0 && Cells[CellUtils.GetIndex(x - 1, y - 1)].IsLive)
                {
                    neighborsCount++;
                }
                if (y < CellUtils.CellSize - 1 && Cells[CellUtils.GetIndex(x - 1, y + 1)].IsLive)
                {
                    neighborsCount++;
                }
            }
            if (x < CellUtils.CellSize - 1)
            {
                if (Cells[CellUtils.GetIndex(x + 1, y)].IsLive)
                {
                    neighborsCount++;
                }
                if (y > 0 && Cells[CellUtils.GetIndex(x + 1, y - 1)].IsLive)
                {
                    neighborsCount++;
                }
                if (y < CellUtils.CellSize - 1 && Cells[CellUtils.GetIndex(x + 1, y + 1)].IsLive)
                {
                    neighborsCount++;
                }
            }
            if (y > 0)
            {
                if (Cells[CellUtils.GetIndex(x, y - 1)].IsLive)
                {
                    neighborsCount++;
                }
            }
            if (y < CellUtils.CellSize - 1)
            {
                if (Cells[CellUtils.GetIndex(x, y + 1)].IsLive)
                {
                    neighborsCount++;
                }
            }
            var isLive = Cells[CellUtils.GetIndex(x, y)].IsLive;
            var newLife = false;

            if (isLive)
            {
                if (neighborsCount == 3 || neighborsCount == 2)
                {
                    newLife = true;
                }
            }
            else
            {
                if (neighborsCount == 3)
                {
                    newLife = true;
                }
            }

            Debug.WriteLine("Cell : x[" + x + "] y[" + y + "] Neighbor[" + neighborsCount + "] Life[" + newLife + "]");

            return newLife;
        }
    }
}

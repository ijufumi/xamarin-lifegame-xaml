using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
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
                    T => CanStart
            )
            .ObservesProperty(() => CanStart);

            StopCommand = new DelegateCommand<string>(
                    async T => await ControlGame(T),
                    T => CanStop
                )
                .ObservesProperty(() => CanStop);

            CellClick = new DelegateCommand<int?>(
                    async T => await CellClicked(T),
                    T => CanCellClick)
                .ObservesProperty(() => CanCellClick);

            IsExecuted = false;
        }

        public bool CanStart => !IsExecuted && HasLiveCells();
        public bool CanStop => IsExecuted;
        public bool CanCellClick => !IsExecuted;

        // 生きているセルが1つでも存在しないと、スタートできないようにするためのもの
        public bool HasLiveCells()
        {
            return Cells.FirstOrDefault(c => c.IsLive) != null;
        }

        private bool _isExecuted;

        public bool IsExecuted
        {
            get { return _isExecuted; }
            set
            {
                // 実行状態によって、ボタンの画像を差し替える
                // このことによって、ボタンが押せるのか押せないのか分かるようになる
                if (value)
                {
                    StartButtonImage = ImageSource.FromFile("start_button_disabled.png");
                    StopButtonImage = ImageSource.FromFile("stop_button.png");
                }
                else
                {
                    StartButtonImage = ImageSource.FromFile("start_button.png");
                    StopButtonImage = ImageSource.FromFile("stop_button_disabled.png");
                }
                SetProperty(ref _isExecuted, value);
            }
        }

        public DelegateCommand<string> StartCommand { get; }

        public DelegateCommand<string> StopCommand { get; }

        public DelegateCommand<int?> CellClick { get; }

        public Cell[] Cells { get; set; }

        private ImageSource _startButtonImage;
        public ImageSource StartButtonImage
        {
            get
            {
                return _startButtonImage;
            }
            private set
            {
                SetProperty(ref _startButtonImage, value);
            }
        }

        private ImageSource _stopButtonImage;
        public ImageSource StopButtonImage
        {
            get
            {
                return _stopButtonImage;
            }
            private set
            {
                SetProperty(ref _stopButtonImage, value);
            }
        }

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

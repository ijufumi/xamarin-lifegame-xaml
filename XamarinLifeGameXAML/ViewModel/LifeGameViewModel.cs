using System.Threading.Tasks;
using System.Diagnostics;
using Prism.Commands;
using Prism.Mvvm;
using XamarinLifeGameXAML.Logic;

namespace XamarinLifeGameXAML.ViewModel
{
    public class LgViewModel : BindableBase
    {

        private bool isExecuted;

        public LgViewModel()
        {
            StartCommand = new DelegateCommand(
                    async () => await ControlGame(),
                    () => !isExecuted
            )
            .ObservesProperty(() => isExecuted);

            StopCommand = new DelegateCommand(
                    async () => await ControlGame(),
                    () => isExecuted
                )
                .ObservesProperty(() => isExecuted);
        }

        public DelegateCommand StartCommand { get; }

        public DelegateCommand StopCommand { get; }

        public Cell[] Cells { get; set; }

        private async Task ControlGame()
        {
            Debug.WriteLine("Call ControlGame : " + isExecuted);
            if (isExecuted)
            {
                isExecuted = false;

            }
            else
            {
                isExecuted = true;
            }
        }
    }
}

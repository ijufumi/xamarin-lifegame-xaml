using System.Threading.Tasks;
using System.Diagnostics;
using Prism.Commands;
using Prism.Mvvm;
using XamarinLifeGameXAML.Logic;

namespace XamarinLifeGameXAML.ViewModels
{
    public class LifeGameViewModel : BindableBase
    {

        public LifeGameViewModel()
        {
            this.StartCommand = new DelegateCommand(
                    executeMethod: async () => await ControlGame(),
                    canExecuteMethod: () => !IsExecuted
            )
            .ObservesProperty(propertyExpression: () => IsExecuted);

            this.StopCommand = new DelegateCommand(
                    executeMethod: async () => await ControlGame(),
                    canExecuteMethod: () => IsExecuted
                )
                .ObservesProperty(propertyExpression: () => IsExecuted);


        }

        private bool isExecuted;
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
            }
            else
            {
                IsExecuted = true;
            }
        }
    }
}

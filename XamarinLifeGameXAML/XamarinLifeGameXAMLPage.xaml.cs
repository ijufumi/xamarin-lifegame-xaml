using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace XamarinLifeGameXAML
{
    public partial class XamarinLifeGameXAMLPage : ContentPage
    {
        public XamarinLifeGameXAMLPage()
        {
            InitializeComponent();
        }

        public void Tapped2(object sender, EventArgs e)
        {
            var vm = (LgViewModel) this.BindingContext;
            Cell cell = (Cell)sender;

            int index = cell.Index;

            Debug.WriteLine(index);
            vm.Cells[index].ChangeState();
        }
    }
}

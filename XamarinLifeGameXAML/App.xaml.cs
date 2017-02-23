using System;
using Prism.Unity;
using Xamarin.Forms;
using XamarinLifeGameXAML.Views;

namespace XamarinLifeGameXAML
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync("LifeGame");
        }

        protected override void RegisterTypes()
        {
            this.Container.RegisterTypeForNavigation<LifeGame>();
        }
    }
}

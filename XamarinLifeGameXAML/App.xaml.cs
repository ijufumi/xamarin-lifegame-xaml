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
            NavigationService.NavigateAsync("NavigationPage/LifeGame");
            //MainPage = new LifeGame();
        }

        protected override void RegisterTypes()
        {
            this.Container.RegisterTypeForNavigation<LifeGame>();
            this.Container.RegisterTypeForNavigation<NavigationPage>();
        }
    }
}

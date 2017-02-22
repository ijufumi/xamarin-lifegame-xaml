using System;
using Prism.Unity;
using Xamarin.Forms;
using XamarinLifeGameXAML.View;

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

            MainPage = new LifeGame();
        }

        protected override void RegisterTypes()
        {
            // TODO
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

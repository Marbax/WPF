using Game2048.Views;
using Ninject;
using System.Windows;

namespace Game2048
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = new StandardKernel();

            MainView view = container.Get<MainView>();
            view.Show();
        }
    }
}
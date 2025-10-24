using System.Windows;
using PDVnet.GestaoProdutos.Data;

namespace PDVnet.GestaoProdutos.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DatabaseInitializer.EnsureDatabase(); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ED_DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Console.WriteLine("Aplication Started!");

            //MainWindow wnd = new MainWindow();
            //wnd.Title = "База сотрудников и департаметов";
            //wnd.Show();
        }
    }
}

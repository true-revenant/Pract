using Pract.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pract
{
    static class Program
    {
        static GameForm gameForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new GameForm());

            gameForm = new GameForm();
            Game.Init(gameForm);
            Game.Draw();
            Application.Run(gameForm);
        }
    }
}

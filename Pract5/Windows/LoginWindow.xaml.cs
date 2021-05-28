using ED_WcfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ED_DesktopClient.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            loginTextBox.Focus();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            //Login = corp_user
            //Password = 123456

            try
            {
                EDService service = new EDService();
                if (service.PostAndCheckCredentials(loginTextBox.Text, passwordBox.Password))
                {
                    MainWindow m = new MainWindow();
                    m.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} : {(ex.InnerException != null ? ex.InnerException.Message : "")}" , "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

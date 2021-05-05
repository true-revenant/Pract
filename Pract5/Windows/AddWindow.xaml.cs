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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        //DepartmentBase dBase;

        public AddWindow()
        {
            InitializeComponent();
        }

        public AddWindow(List<Department> depList)
        {
            InitializeComponent();
            this.Title = "Новый сотрудник";
            newEmployUserControl.initDepartmentBase(depList);
            newEmployUserControl.depsComboBox.SelectedIndex = 0;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Owner;

            var newEmp = new Employee(newEmployUserControl.nameTextBox.Text,
                                      newEmployUserControl.lastnameTextBox.Text, 30,
                                      (bool)newEmployUserControl.activeCheckBox.IsChecked,
                                      Int32.Parse(newEmployUserControl.stageTextBox.Text),
                                      (Department)newEmployUserControl.depsComboBox.SelectedItem);

            if (main.AddEmployeeToBase(newEmp) > 0)
            {
                main.EmployeesList.Add(newEmp);
                MessageBox.Show("Запись добавлена!", "Добавление записи о сотруднике", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
        }
    }
}

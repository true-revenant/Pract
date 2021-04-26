using Pract5.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pract5.UserControls
{
    /// <summary>
    /// Interaction logic for EmployeeUserControl.xaml
    /// </summary>
    public partial class EmployeeUserControl : UserControl
    {
        Employee employee;
        DepartmentBase departmentBase;

        public EmployeeUserControl()
        {
            InitializeComponent();
        }

        public void initDepartmentBase(DepartmentBase dBase)
        {
            departmentBase = dBase;
            depsComboBox.ItemsSource = departmentBase.Departments;
        }

        public void ShowEmployeeInfo(Employee emp)
        {
            employee = emp;

            nameTextBox.Text = emp.FirstName;
            lastnameTextBox.Text = emp.LastName;
            depsComboBox.SelectedItem = emp.getDep();
            activeCheckBox.IsChecked = emp.IsActive;
            stageTextBox.Text = emp.StageYears.ToString();
        }

        public void UpdateEmployeeInfo()
        {
            employee.FirstName = nameTextBox.Text;
            employee.LastName = lastnameTextBox.Text;
            employee.IsActive = (bool)activeCheckBox.IsChecked;
            employee.StageYears = Int32.Parse(stageTextBox.Text);
            employee.SetDep((Department)depsComboBox.SelectedItem);
        }
    }
}

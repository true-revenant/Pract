﻿using Pract5.Classes;
using Pract5.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Pract5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeeBase employeeBase;
        DepartmentBase departmentBase;
        int selectedIndex;
        
        public Employee SelectedEmployee { get; set; }

        public ObservableCollection<Employee> EmployeesList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "База сотрудников и департаментов";
            this.DataContext = this;

            departmentBase = new DepartmentBase();
            employeeBase = new EmployeeBase(departmentBase.Departments);
            EmployeesList = employeeBase.Employees;

            employeeUserControl.initDepartmentBase(departmentBase);
            SetEmployeeControlStatus(false);
        }

        public void AddEmployeeToBase(Employee emp)
        {
            //EmployeesList.Add(emp);
            employeeBase.Add(emp);
        }
        private void SetEmployeeControlStatus(bool is_enabled)
        {
            //addButton.IsEnabled = is_enabled;
            deleteButton.IsEnabled = is_enabled;
            editButton.IsEnabled = is_enabled;

            if (!is_enabled)
            {
                employeeUserControl.lastnameTextBox.Text = "";
                employeeUserControl.nameTextBox.Text = "";
                employeeUserControl.depsComboBox.Text = "";
                employeeUserControl.stageTextBox.Text = "";
                employeeUserControl.activeCheckBox.IsChecked = is_enabled;
            }

            employeeUserControl.lastnameTextBox.IsEnabled = is_enabled;
            employeeUserControl.nameTextBox.IsEnabled = is_enabled;
            employeeUserControl.depsComboBox.IsEnabled = is_enabled;
            employeeUserControl.activeCheckBox.IsEnabled = is_enabled;
            employeeUserControl.stageTextBox.IsEnabled = is_enabled;
        }

        private void employeeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                SelectedEmployee = (Employee)employeeListView.SelectedItem;
                employeeUserControl.Emp = (Employee)SelectedEmployee.Clone();
                selectedIndex = employeeListView.SelectedIndex;
                SetEmployeeControlStatus(true);
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (employeeListView.SelectedItems.Count == 1)
            {
                employeeBase.Update(employeeUserControl.Emp, selectedIndex);
                MessageBox.Show("Запись обновлена!", "Изменение записи о сотруднике", MessageBoxButton.OK, MessageBoxImage.Information);
                employeeListView.SelectedIndex = selectedIndex;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (employeeListView.SelectedItems.Count == 1 && 
                MessageBox.Show("Уверены что хотите удалит запись о сотркднике?", "Удаление записи сотрудника", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                employeeBase.Delete(SelectedEmployee);
                SetEmployeeControlStatus(false);
                MessageBox.Show("Запись удалена!", "Изменение записи о сотруднике", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow(departmentBase);
            addWindow.Owner = this;
            addWindow.ShowDialog();
        }
    }
}
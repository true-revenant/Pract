using ED_WcfService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ED_DesktopClient.UserControls
{
    /// <summary>
    /// Interaction logic for EmployeeUserControl.xaml
    /// </summary>
    public partial class EmployeeUserControl : UserControl, INotifyPropertyChanged
    {
        Employee _employee;

        public event PropertyChangedEventHandler PropertyChanged;

        public Employee Emp
        {
            get { return _employee; }
            set
            {
                _employee = value;
                HandleNotifyPropertyChanged();
            }
        }

        public List<Department> Deps { get; set; }

        public EmployeeUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void initDepartmentBase(List<Department> depList)
        {
            //Deps = dBase.Departments;
            Deps = depList;
        }

        public void HandleNotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}

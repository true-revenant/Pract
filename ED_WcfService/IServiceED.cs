using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ED_WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceED
    {        
        [OperationContract]
        ObservableCollection<Employee> LoadEmployees(List<Department> departmentsList);

        [OperationContract]
        int AddEmployee(Employee empl);

        [OperationContract]
        int RemoveEmployee(Employee empl);

        [OperationContract]
        Employee UpdateEmployee(Employee empl);

        [OperationContract]
        List<Department> LoadDepartments();

        bool PostAndCheckCredentials(string login, string password);
    }

    [DataContract]
    public class Department
    {
        int _department_ID;

        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int ID
        {
            get { return _department_ID; }
            set { _department_ID = value; }
        }

        public override string ToString()
        {
            return Title;
        }
    }

    [DataContract]
    public class Employee : INotifyPropertyChanged, ICloneable
    {
        Department _dep;
        string _firstName;
        string _lastName;
        int _age;
        bool _isActive;
        int _stageYears;
        int _employee_ID;

        public event PropertyChangedEventHandler PropertyChanged;

        public Department Dep
        {
            get { return _dep; }
            set
            {
                _dep = value;
                HandleNotifyPropertyChanged();
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                HandleNotifyPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                HandleNotifyPropertyChanged();
            }
        }
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                HandleNotifyPropertyChanged();
            }
        }
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                HandleNotifyPropertyChanged();
            }
        }
        public int StageYears
        {
            get { return _stageYears; }
            set
            {
                _stageYears = value;
                HandleNotifyPropertyChanged();
            }
        }
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
        public string DepTitle
        {
            get { return Dep.Title; }
        }

        public Employee(string firstName, string lastName, int age, bool isActive, int stageYears, Department dep)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            IsActive = isActive;
            StageYears = stageYears;

            Dep = dep;
        }

        public int getEmployeeID()
        {
            return _employee_ID;
        }

        public void SetEmployeeID(int id)
        {
            _employee_ID = id;
        }

        public void HandleNotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

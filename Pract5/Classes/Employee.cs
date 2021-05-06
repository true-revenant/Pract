using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pract5.Classes
{
    public class Employee : INotifyPropertyChanged, ICloneable
    {
        Department _dep;
        string _firstName;
        string _lastName;
        int _age;
        bool _isActive;
        int _stageYears;
        
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

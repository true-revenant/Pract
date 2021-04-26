using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract5.Classes
{
    public class Employee
    {
        Department department;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public int StageYears { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public string DepTitle 
        {
            get { return department.Title; }
        }

        public Employee(string firstName, string lastName, int age, bool isActive, int stageYears, Department dep)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            IsActive = isActive;
            StageYears = stageYears;

            department = dep;
        }

        public Department getDep()
        {
            return department;
        }

        public void SetDep(Department dep)
        {
            department = dep;
        }
    }
}

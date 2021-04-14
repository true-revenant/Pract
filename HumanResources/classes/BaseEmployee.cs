using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.classes
{
    abstract class BaseEmployee : IComparable<BaseEmployee>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }

        public BaseEmployee(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        protected abstract float GetPayment();

        public int CompareTo(BaseEmployee emp)
        {
            return this.Age - emp.Age;
        }
    }
}

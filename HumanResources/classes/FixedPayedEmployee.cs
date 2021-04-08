using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.classes
{
    class FixedPayedEmployee : BaseEmployee
    {
        private float monthPayment;

        public FixedPayedEmployee(string firstName, string lastName, int age, float monthPayment) : base(firstName, lastName, age) 
        {
            this.monthPayment = monthPayment;
        }
        
        protected override float GetPayment()
        {
            return monthPayment;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}\t Возраст: {Age}\t Месячный оклад:{GetPayment()} руб.";
        }
    }
}

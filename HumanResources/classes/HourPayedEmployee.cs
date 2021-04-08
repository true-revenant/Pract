using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.classes
{
    class HourPayedEmployee : BaseEmployee
    {
        private float hourPayment;

        public HourPayedEmployee(string firstName, string lastName, int age, float hourPayment) : base(firstName, lastName, age)
        {
            this.hourPayment = hourPayment;
        }

        protected override float GetPayment()
        {
            return 20.8f * 8 * hourPayment;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}\t Возраст: {Age}\t Месячный оклад:{GetPayment()} руб.";
        }
    }
}

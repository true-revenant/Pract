using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.classes
{
    class EmployeeArray : IEnumerable
    {
        private BaseEmployee[] employees;

        public EmployeeArray()
        {
            employees = new BaseEmployee[10]
            {
                new FixedPayedEmployee("Брунгивальд", "Джонсон", 30, 12000),
                new FixedPayedEmployee("Аристарх", "Сигизмундов", 25, 10000),
                new FixedPayedEmployee("Роман", "Зильман", 27, 40000),
                new FixedPayedEmployee("Виктор", "Пересветов", 40, 1000),
                new FixedPayedEmployee("Пенелопа", "Круз", 20, 1000000),
                new HourPayedEmployee("Лео", "Мастрояни", 60, 5000),
                new HourPayedEmployee("Декард", "Каин", 160, 5000),
                new HourPayedEmployee("Лорд", "Рейден", 200, 7000),
                new HourPayedEmployee("Сильвия", "Сильвстедт", 30, 34000),
                new HourPayedEmployee("Арвен", "Арагорновна", 23, 23000)
            };
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < employees.Length; i++)
                yield return employees[i];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract5.Classes
{
    public class EmployeeBase
    {
        public ObservableCollection<Employee> Employees { get; set; }

        private readonly int CHAR_MIN = 65;
        private readonly int CHAR_MAX = 90;
        private readonly Random rnd = new Random();

        private List<Department> departmentsList;

        public EmployeeBase(List<Department> departmentsList)
        {
            this.departmentsList = departmentsList;
            GenerateList(50);
        }

        private void GenerateList(int count)
        {
            Employees = new ObservableCollection<Employee>();

            for(int i = 1; i <= count; i++)
            {
                Employees.Add(new Employee(GenerateName(rnd.Next(10, 20)),
                                            GenerateName(rnd.Next(10, 20)),
                                            GenerateAge(), GenerateActiveStatus(),
                                            GenerateStageYears(),
                                            departmentsList[rnd.Next(0, departmentsList.Count - 1)]));
            }
        }

        private string GenerateName(int amount)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i <= amount; i++)
                sb.Append((char)rnd.Next(CHAR_MIN, CHAR_MAX));

            return sb.ToString();
        }

        private int GenerateAge()
        {
            return rnd.Next(20, 60);
        }

        private bool GenerateActiveStatus()
        {
            return rnd.Next(0, 1) == 0 ? false : true;
        }

        private int GenerateStageYears()
        {
            return rnd.Next(1, 20);
        }
    }
}

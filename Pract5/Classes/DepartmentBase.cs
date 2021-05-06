using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract5.Classes
{
    public class DepartmentBase
    {
        public List<Department> Departments;
        private readonly int CHAR_MIN = 65;
        private readonly int CHAR_MAX = 90;

        private readonly Random rnd = new Random();

        public DepartmentBase()
        {
            GenerateList(15);
        }

        private void GenerateList(int count)
        {
            Departments = new List<Department>();

            for (int i = 1; i <= count; i++)
            {
                Department dep = new Department(GenerateTitle(30), GenerateActiveStatus());

                Departments.Add(dep);
            }
        }

        private string GenerateTitle(int amount)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i <= amount; i++)
                sb.Append((char)rnd.Next(CHAR_MIN, CHAR_MAX));

            return sb.ToString();
        }

        private bool GenerateActiveStatus()
        {
            return rnd.Next(0, 1) == 0 ? false : true;
        }


    }
}

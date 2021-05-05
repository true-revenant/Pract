using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_DBFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            DepartmentBase.ClearTableDB();
            DepartmentBase.FillTableDB(30);

            EmployeeBase.ClearTableDB();
            EmployeeBase.FillTableDB(50, DepartmentBase.departments);

            Console.ReadKey();
        }
    }
}

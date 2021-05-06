using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract5.Classes
{
    public class Department
    {
        int _department_ID;

        public string Title { get; set; }
        public bool IsActive { get; set; }

        public Department(string title, bool isActive)
        {
            Title = title;
            IsActive = isActive;
        }

        public int GetDepartmentID()
        {
            return _department_ID;
        }

        public void SetDepartmentID(int id)
        {
            _department_ID = id;
        }
        public override string ToString()
        {
            return Title;
        }
    }
}

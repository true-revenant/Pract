using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract5.Classes
{
    public class Department
    {
        public string Title { get; set; }

        public bool IsActive { get; set; }

        public Department(string title, bool isActive)
        {
            Title = title;
            IsActive = isActive;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}

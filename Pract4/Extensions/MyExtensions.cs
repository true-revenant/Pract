using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract4.Extensions
{
    public static class MyExtensions
    {
        public static int ElementCount(this List<int> self_list, int element)
        {
            int result = 0;

            foreach(var el in self_list)
                if (el == element) result++;

            return result;
        }

        public static int ElementCount(this ArrayList self_list, object element)
        {
            int result = 0;

            foreach(var el in self_list)
                if (element.Equals(el)) result++;

            return result;
        }


    }
}

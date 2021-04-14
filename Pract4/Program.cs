using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pract4.Extensions;

namespace Pract4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Задание 2 а");
            Console.WriteLine("******************************");
            List<int> intList = new List<int>();
            intList.AddRange(new int[] { 1, 2, 2, 2, 3, 4, 5, 6, 11, 11});
            Task2a(intList);

            Console.WriteLine();

            Console.WriteLine("Задание 2 б");
            Console.WriteLine("******************************");
            ArrayList arrList = new ArrayList();
            arrList.AddRange(new object[] { 1, 2, 2, "str1", "str2" , new Random(), 0.5, "str1", 0.5, 0.45, new StringBuilder("str"), new StringBuilder("str") });
            Task2b(arrList);

            Console.WriteLine();

            Console.WriteLine("Задание 2 б");
            Console.WriteLine("******************************");
            Task2c(intList);

            //Task3();
            Console.ReadKey();
        }

        static void Task2a(List<int> _list)
        {
            Dictionary<int, int> resDict = new Dictionary<int, int>();

            foreach(var l in _list)
            {
                int value = _list.ElementCount(l);
                if (!resDict.ContainsKey(l)) resDict.Add(l, value);
            }

            foreach(var d in resDict)
                Console.WriteLine($"Элемент {d.Key} , кол-во в коллекции - {d.Value}");
        }

        static void Task2b(ArrayList _list)
        {
            Dictionary<object, int> resDict = new Dictionary<object, int>();

            foreach (var l in _list)
            {
                int value = _list.ElementCount(l);
                if (!resDict.ContainsKey(l)) resDict.Add(l, value);
            }

            foreach (var d in resDict)
                Console.WriteLine($"Элемент {d.Key} , кол-во в коллекции - {d.Value}");
        }

        static void Task2c(List<int> _list)
        {
            var result = from l in _list
                         select _list.ElementCount(l);

            Dictionary<object, int> resDict = new Dictionary<object, int>();

            int i = 0;
            foreach (var r in result)
            {
                if (!resDict.ContainsKey(_list[i])) resDict.Add(_list[i], r);
                i++;
            }

            foreach (var d in resDict)
                Console.WriteLine($"Элемент {d.Key} , кол-во в коллекции - {d.Value}");
        }

        static void Task3()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
              {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
              };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });

            foreach (var pair in dict)
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            Console.WriteLine();

            foreach (var pair in d)
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
        }
    }
}

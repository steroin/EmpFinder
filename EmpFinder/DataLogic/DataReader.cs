using EmpFinder.Graph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmpFinder.Data
{
    static class DataReader
    {
        public static IGraph<int> BuildGraph()
        {
            IGraph<int> graph = null;

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EmpFinder.Data.Employees.all.txt"))
            {
                var reader = new StreamReader(stream);
                string line = reader.ReadLine();
                int count = 0;

                while (line != null)
                {
                    count++;
                    line = reader.ReadLine();
                }
                graph = new AdjacencyListGraph<int>(count);
                reader.Close();
            }

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EmpFinder.Data.Publications.all.txt"))
            {
                var reader = new StreamReader(stream);
                string line = reader.ReadLine();

                while(line!=null)
                {
                    string[] arr = line.Split(',');

                    for(int i = 1; i< arr.Length; i++)
                    {
                        for(int j = 1; j<arr.Length; j++)
                        {
                            if (i != j) graph.AddEdge(int.Parse(arr[i]), int.Parse(arr[j]), 1, int.Parse(arr[0]));
                        }
                    }
                    line = reader.ReadLine();
                }

                reader.Close();
            }

            return graph;
        }

        public static Dictionary<int, Employee> ReadEmployees()
        {
            var employees = new Dictionary<int, Employee>();
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EmpFinder.Data.Employees.all.txt"))
            {
                var reader = new StreamReader(stream);
                string line = reader.ReadLine();

                while (line != null)
                {
                    string[] arr = line.Split(',');
                    employees.Add(int.Parse(arr[0]), new Employee(arr[2], arr[1]));
                    line = reader.ReadLine();
                }
                reader.Close();
            }

            return employees;
        }

        public static Dictionary<int, string> ReadPublicationsNames()
        {
            var publications = new Dictionary<int, string>();
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EmpFinder.Data.Publications.names.txt"))
            {
                var reader = new StreamReader(stream);
                string line = reader.ReadLine();

                while (line != null)
                {
                    string[] arr = line.Split(new string[] { "___" }, StringSplitOptions.None);
                    publications.Add(int.Parse(arr[0]), arr[1]);
                    line = reader.ReadLine();
                }
                reader.Close();
            }

            return publications;
        }
    }
}

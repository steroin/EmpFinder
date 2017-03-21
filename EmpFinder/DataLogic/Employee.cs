using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpFinder
{
    class Employee
    {
        public int ID { get; }
        public string Name { get; }
        public string Title { get; }
        public List<Employee> Adjacents { get; }
        public Employee(int id, string name, string title)
        {
            ID = id;
            Name = name;
            Title = title;
            Adjacents = new List<Employee>();
        }

        public override string ToString()
        {
            return Title + " " + Name;
        }
    }
}

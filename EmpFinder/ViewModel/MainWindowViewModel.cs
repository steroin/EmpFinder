using EmpFinder.Data;
using EmpFinder.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EmpFinder
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SearchForConnections { get; set; }
        public ICommand ClearConsole { get; set; }
        public ICommand ShowEmployeesList { get; set; }

        string _FirstEmployee = null;
        public string FirstEmployee
        {
            get
            {
                return _FirstEmployee;
            }
            set
            {
                _FirstEmployee = value;
                OnPropertyChanged("FirstEmployee");
            }
        }
        string _SecondEmployee = null;
        public string SecondEmployee
        {
            get
            {
                return _SecondEmployee;
            }
            set
            {
                _SecondEmployee = value;
                OnPropertyChanged("SecondEmployee");
            }
        }
        string _ConsoleOutput = null;
        public string ConsoleOutput
        {
            get
            {
                return _ConsoleOutput;
            }
            set
            {
                _ConsoleOutput = value;
                OnPropertyChanged("ConsoleOutput");
            }
        }

        IGraph<int> graph;
        Dictionary<int, Employee> employees;
        IDictionary<int, string> publicationsNames;


        public MainWindowViewModel()
        {
            graph = DataReader.BuildGraph();
            employees = DataReader.ReadEmployees();
            publicationsNames = DataReader.ReadPublicationsNames();
            PropertyChanged = null;
            SearchForConnections = new RelayCommand(DoSearchForConnections, CanSearchForConnections);
            ClearConsole = new RelayCommand(DoClearConsole, CanClearConsole);
            ShowEmployeesList = new RelayCommand(DoShowEmployeesList, CanShowEmployeesList);
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        void DoSearchForConnections(object parameter)
        {
            if (FirstEmployee == "" || SecondEmployee == "" || FirstEmployee == null || SecondEmployee == null) return;

            Employee first = FindEmployeeByName(FirstEmployee);
            Employee second = FindEmployeeByName(SecondEmployee);

            DoClearConsole(null);

            if (first == null)
            {
                ConsoleOutput = "Nie znaleziono pracownika '" + FirstEmployee + "'";
                return;
            }
            if (second == null)
            {
                ConsoleOutput = "Nie znaleziono pracownika '" + SecondEmployee + "'";
                return;
            }

            int[] path = graph.FindShortestPath(first.ID, second.ID);

            if(path == null)
            {
                ConsoleOutput = "Nie istnieje połączenie między " + first + ", a " + second + ".";
                return;
            }

            for (int i = 0; i < path.Length; i++)
            {
                if (i + 1 < path.Length)
                {
                    int pubNum = graph.GetEdgeInfo(path[i], path[i + 1]);
                    ConsoleOutput += (i + 1) + ". " + employees[path[i]] + " -> " + employees[path[i + 1]] + " ("+publicationsNames[pubNum]+")\n";
                }
            }

        }
        bool CanSearchForConnections(object parameter)
        {
            return true;
        }
        void DoClearConsole(object parameter)
        {
            ConsoleOutput = "";
        }
        bool CanClearConsole(object parameter)
        {
            return ConsoleOutput!="";
        }
        void DoShowEmployeesList(object parameter)
        {
            DoClearConsole(null);
            foreach(var e in employees)
            {
                ConsoleOutput += (e.Key+1) + ". " + e.Value.Title + " " + e.Value.Name + "\n";
            }
        }
        bool CanShowEmployeesList(object parameter)
        {
            return true;
        }
        private Employee FindEmployeeByName(string name)
        {
            Employee ret = null;
            int found = 0;
            string lowerName = name.Trim().ToLower();

            foreach (var e in employees.Values)
            {
                string[] arr = e.Name.Trim().ToLower().Split(' ');
                Console.WriteLine("checking: "+name+" contains "+arr[0]+" "+arr[1]);
                if (lowerName.Contains(arr[0]) && lowerName.Contains(arr[1]))
                {
                    if (found > 0) return null;
                    found++;
                    ret = e;
                }
            }

            return ret;
        }
    }
}

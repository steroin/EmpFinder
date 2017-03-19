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
            MessageBox.Show("DoSearchForConnections, FirstEmployee: " + FirstEmployee + ", SecondEmployee: " + SecondEmployee);
        }
        bool CanSearchForConnections(object parameter)
        {
            return true;
        }
        void DoClearConsole(object parameter)
        {
            ConsoleOutput = "";
            MessageBox.Show("DoClearConsole, FirstEmployee: " + FirstEmployee + ", SecondEmployee: " + SecondEmployee);
        }
        bool CanClearConsole(object parameter)
        {
            return true;
        }
        void DoShowEmployeesList(object parameter)
        {
            MessageBox.Show("DoShowEmployeeList, FirstEmployee: "+FirstEmployee+", SecondEmployee: "+SecondEmployee);
        }
        bool CanShowEmployeesList(object parameter)
        {
            return true;
        }


    }
}

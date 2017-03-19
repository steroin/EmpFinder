using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpFinder.Graph
{
    interface IGraph
    {
        void AddEdge(int vertexA, int vertexB, double weight);
        int[] FindPaths(int vertex);
        int[] FindShortestPath(int vertexA, int vertexB);
        double GetWeight(int vertexFrom, int vertexTo);
    }
}

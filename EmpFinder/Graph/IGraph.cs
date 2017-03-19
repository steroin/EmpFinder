using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpFinder.Graph
{
    interface IGraph<EdgeInfo>
    {
        void AddEdge(int vertexA, int vertexB, double weight, EdgeInfo info);
        int[] FindPaths(int vertex);
        int[] FindShortestPath(int vertexA, int vertexB);
        double GetWeight(int vertexFrom, int vertexTo);
        EdgeInfo GetEdgeInfo(int vertexA, int vertexB);

    }
}

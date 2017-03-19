using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpFinder.Graph
{
    interface IGraph<WeightType> where WeightType : IWeightable<WeightType>
    {
        void AddEdge(int vertexA, int vertexB, WeightType weight);
        int[] FindPaths(int vertex);
        int[] FindShortestPath(int vertexA, int vertexB);
        WeightType GetWeight(int vertexFrom, int vertexTo);

    }
}

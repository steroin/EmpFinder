using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpFinder.Graph
{
    public class AdjacencyListGraph : IGraph
    {
        Dictionary<int, double>[] vertices;
        bool directed;

        public AdjacencyListGraph(int n)
        {
            vertices = new Dictionary<int, double>[n];
            for (int i = 0; i < n; i++) vertices[i] = new Dictionary<int, double>();
        }

        public AdjacencyListGraph(int n, bool d=false)
        {
            vertices = new Dictionary<int, double>[n];
            directed = d;
        }
        public void AddEdge(int vertexA, int vertexB, double weight)
        {
            if (vertexA < 0 || vertexA >= vertices.Length) throw new VertexNotExistsException(vertexA);
            else if (vertexB < 0 || vertexB >= vertices.Length) throw new VertexNotExistsException(vertexB);

            if (vertices[vertexA].Keys.Contains(vertexB))
                vertices[vertexA][vertexB] = weight;
            else vertices[vertexA].Add(vertexB, weight);
            
            if (!directed)
            {
                if (vertices[vertexB].Keys.Contains(vertexA))
                    vertices[vertexB][vertexA] = weight;
                else vertices[vertexB].Add(vertexA, weight);
            }
        }

        //Dijkstra
        public int[] FindPaths(int source)
        {
            var distances = new double[vertices.Length];
            var prev = new int[vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                distances[i] = double.PositiveInfinity;
                prev[i] = -1;
            }

            distances[source] = 0;

            var queue = new SortedList<double, int>(new AllowDuplicateComparer<double>());
            queue.Add(0, source);
            while (queue.Count > 0)
            {
                var first = queue.ElementAt(0);
                int vertex = first.Value;
                double weight = first.Key;
                queue.RemoveAt(0);

                double dist = distances[vertex];

                foreach (int adj in vertices[vertex].Keys)
                {
                    if (dist + vertices[vertex][adj] < distances[adj])
                    {
                        distances[adj] = dist + vertices[vertex][adj];
                        prev[adj] = vertex;
                        queue.Add(distances[adj], adj);
                    }
                }
            }
            foreach(double d in distances)Console.WriteLine(d);
            return prev;
        }

        public int[] FindShortestPath(int vertexA, int vertexB)
        {
            int[] paths = FindPaths(vertexB);
            var list = new List<int>();
            int currentVertex = vertexA;
            list.Add(vertexA);

            while(currentVertex!=vertexB)
            {
                if (currentVertex == -1) return null;
                currentVertex = paths[currentVertex];
                list.Add(currentVertex);
            }

            return list.ToArray();
        }

        public double GetWeight(int vertexFrom, int vertexTo)
        {
            if (!vertices[vertexFrom].Keys.Contains(vertexTo)) return double.NaN;
            return vertices[vertexFrom][vertexTo];
        }
    }
}

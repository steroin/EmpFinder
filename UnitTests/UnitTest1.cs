using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmpFinder.Graph;
using System.Diagnostics;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var g = new AdjacencyListGraph(6);

            g.AddEdge(0, 1, 8);
            g.AddEdge(0, 2, 5);
            g.AddEdge(0, 4, 10);
            g.AddEdge(0, 5, 1);
            g.AddEdge(1, 3, 4);
            g.AddEdge(1, 4, 1);
            g.AddEdge(2, 3, 2);
            g.AddEdge(2, 4, 1);
            g.AddEdge(2, 5, 1);
            g.AddEdge(3, 4, 7);

            int[] path = g.FindPaths(3);
            for(int i = 0; i < path.Length; i++)
            {
                Console.WriteLine(i + ": " + path[i]);
            }
            int[] paths = g.FindShortestPath(0, 3);
            string s = "";
            foreach(int n in paths)s+=n;

           Assert.AreEqual("023", s);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpFinder.Graph
{
    class VertexNotExistsException : Exception
    {
        public VertexNotExistsException(int num) : base("W grafie nie istnieje wierzchołek numer " + num) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpFinder.Graph
{
    class AllowDuplicateComparer<Key> : IComparer<Key> where Key : IComparable
    {
        public int Compare(Key x, Key y)
        {
            int res = x.CompareTo(y);
            return res==0?1:res;
        }
    }
}

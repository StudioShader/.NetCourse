using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{
    internal class HashElement<T> where T : IComparable
    {
        public T Content { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsDeleted { get; set; }
    }
}
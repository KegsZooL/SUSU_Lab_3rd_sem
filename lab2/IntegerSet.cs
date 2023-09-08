using System.Collections.Generic;
using System.Linq;

namespace lab_2
{
    class IntegerSet
    {
        public HashSet<int> _HashSet { get; private set; }

        public IntegerSet() => _HashSet = new HashSet<int>();

        public IntegerSet(IEnumerable<int> collections) => _HashSet = new HashSet<int>(collections);

        private static IntegerSet Union(IntegerSet set_1, IntegerSet set_2)
        {
            IntegerSet unionHashSet = new IntegerSet(set_1._HashSet.Concat(set_2._HashSet));
            return unionHashSet;
        } 
        private static IntegerSet Union(IntegerSet set_1, int value)
        {
            set_1._HashSet.Add(value);
            return set_1;
        }

        public static IntegerSet operator +(IntegerSet set_1, IntegerSet set_2) 
        {
            return Union(set_1, set_2);
        }
        
        public static IntegerSet operator +(IntegerSet set_1, int value) 
        {
            return Union(set_1, value);
        }
    }
}

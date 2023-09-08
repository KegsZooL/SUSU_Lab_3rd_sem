using System.Collections.Generic;
using System.Linq;

namespace lab_2
{
    class IntegerSet
    {
        public HashSet<int> _HashSet { get; set; }

        public IntegerSet() => _HashSet = new HashSet<int>();

        public IntegerSet(IEnumerable<int> collections) => _HashSet = new HashSet<int>(collections);

        private static IntegerSet Union(IntegerSet set_1, IntegerSet set_2)
        {
            IntegerSet unionHashSet = new IntegerSet(set_1._HashSet.Concat(set_2._HashSet));
            return unionHashSet;
        } 

        private static IntegerSet Union(IntegerSet set, int value)
        {
            set._HashSet.Add(value);
            return set;
        }

        private static IntegerSet Remove(IntegerSet set, int value) 
        {
            set._HashSet.Remove(value);
            return set;
        }

        private static IntegerSet Remove(IntegerSet set_1, IntegerSet set_2) 
        {
            set_1._HashSet.ExceptWith(set_2._HashSet);
            return set_1;
        }

        //private static bool Compare(IntegerSet set_1, IntegerSet set_2) 
        //{
        //    return true;
        //}

        public static IntegerSet operator +(IntegerSet set_1, IntegerSet set_2){
            return Union(set_1, set_2);
        }
        
        public static IntegerSet operator +(IntegerSet set, int value) {
            return Union(set, value);
        }

        public static IntegerSet operator -(IntegerSet set, int value) {
            return Remove(set, value);
        }

        public static IntegerSet operator -(IntegerSet set_1, IntegerSet set_2) {
            return Remove(set_1, set_2);
        }

        //public static IntegerSet operator <=(IntegerSet set_1, IntegerSet set_2) {
        //    return Compare(set_1, set_2);
        //}

    }
}

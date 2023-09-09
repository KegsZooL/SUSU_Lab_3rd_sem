using System.Collections.Generic;
using System.Linq;

namespace lab_2
{
    class IntegerSet
    {
        public HashSet<int> _HashSet { get; set; } //Публичное(т.к необходимо измерять длину для проверки коррекци удаления элемента) свойство для хранения HashSet

        public IntegerSet() => _HashSet = new HashSet<int>(); // Конструктор для создания нового объекта HashSet с пустыми значениями

        public IntegerSet(IEnumerable<int> collections) => _HashSet = new HashSet<int>(collections); // Перегрузка конструктора для создания нового объекта HashSet с заданными значениями

        private static IntegerSet Union(ref IntegerSet set_1, ref IntegerSet set_2) // Объединение множеств
        {
            IntegerSet unionHashSet = new IntegerSet(set_1._HashSet.Concat(set_2._HashSet));
            return unionHashSet;
        } 

        private static IntegerSet Union(ref IntegerSet set, ref int value) // Добавление элемента в множество
        {
            set._HashSet.Add(value);
            return set;
        }

        private static IntegerSet Remove(ref IntegerSet set, ref int value) // Удаление элемента в множестве
        {
            set._HashSet.Remove(value);
            return set;
        }

        private static IntegerSet Remove(ref IntegerSet set_1, ref IntegerSet set_2) // Разность множеств
        {
            set_1._HashSet.ExceptWith(set_2._HashSet);
            return set_1;
        }

        private static int Comprasion(ref IntegerSet set_1, ref IntegerSet set_2) // Сравнение множеств по общей сумме элементов 
        {
            int[] summs = new int[2];

            HashSet<int>[] sets = new HashSet<int>[] { set_1._HashSet, set_2._HashSet };

            for (int i = 0; i < summs.Length; i++)
            {
                foreach(int num in sets[i]) 
                {
                    summs[i] += num;
                }
            }

            return summs[0] - summs[1];
        }


        //Перегрузка операторов(+, -, ==, !=, >=, <=, >, <)

        public static IntegerSet operator +(IntegerSet set_1, IntegerSet set_2) => Union(ref set_1, ref set_2);

        public static IntegerSet operator +(IntegerSet set, int value) => Union(ref set, ref value);

        public static IntegerSet operator -(IntegerSet set, int value) => Remove(ref set, ref value);

        public static IntegerSet operator -(IntegerSet set_1, IntegerSet set_2) => Remove(ref set_1, ref set_2);

        public static bool operator ==(IntegerSet set_1, IntegerSet set_2) => set_1._HashSet.SetEquals(set_2._HashSet);

        public static bool operator !=(IntegerSet set_1, IntegerSet set_2) => set_1._HashSet.SetEquals(set_2._HashSet) ? false : true; // Если метод SetEquals() вернет true, то множества равны, иначе множества не равны

        public static bool operator >=(IntegerSet set_1, IntegerSet set_2) => Comprasion(ref set_1, ref set_2) >= 0 ? true : false;
      
        public static bool operator <=(IntegerSet set_1, IntegerSet set_2) => Comprasion(ref set_1, ref set_2) <= 0 ? true : false;        
        
        public static bool operator >(IntegerSet set_1, IntegerSet set_2) => Comprasion(ref set_1, ref set_2) > 0 ? true : false;
      
        public static bool operator <(IntegerSet set_1, IntegerSet set_2) => Comprasion(ref set_1, ref set_2) < 0 ? true : false;

    }
}

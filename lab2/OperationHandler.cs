using Sharprompt;
using System.Collections.Generic;
using System.Linq;

namespace lab_2
{
    public class OperationHandler
    {
        public void Start()
        {
            Dictionary<string, IntegerSet> dict = new Dictionary<string, IntegerSet>() 
            {
                {"1",  new IntegerSet(new List<int> { 1, 2, 3, 4, 5 }) },
                {"2",  new IntegerSet(new List<int> { 3, 2, 7, 4, 1 }) }
            };

            var typeOfOperation = Prompt.Select("Выберите тип операции", new[] {"[1] Добавить элемент в множество.", 
                "[2] Удалить элемент в множестве.", "[3] Объединить множества.", "[4] Сравнить множества.", "[5] Разность множеств."});

            switch (typeOfOperation[1])
            {
                case ('1'):

                    var definiteSet = Prompt.Select("В какое множество вы хотите добавить элемент?", dict.Keys);
                    var value = Prompt.List<int>($"Введите число, которое хотите добавить в множество №{definiteSet}");

                    dict[definiteSet] = dict[definiteSet] + value.ToArray<int>()[0];

                    break;

                case ('2'):

                    break;

                case ('3'):

                    break;

                case ('4'):

                    break;

                case ('5'):

                    break;
            }
        }
    }
}

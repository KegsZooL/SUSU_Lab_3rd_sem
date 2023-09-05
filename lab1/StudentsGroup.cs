using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1
{
    public class DoublyLinkedList
    {
        public string FullName { get; set; }

        public string Phone { get; }

        public DateTime BirthDate { get; set; }

        public DoublyLinkedList Next { get; set; }

        public DoublyLinkedList Previous { get; set; }

        public DoublyLinkedList(string _fullName, string _phone, DateTime _birthDate) 
        {
            FullName = _fullName;
            Phone = _phone;
            BirthDate = _birthDate;
        }
    }

    public class StudentsGroup
    {
        DoublyLinkedList header, currentNode, previous = null;

        public void Add(string fullName, string phone, DateTime birthDate)
        {
            if(header == null) 
            {
                header = new DoublyLinkedList( fullName, phone, birthDate);
                currentNode = header;

                return;
            }

            currentNode.Next = new DoublyLinkedList(fullName, phone, birthDate);
            currentNode.Previous = previous;

            if(currentNode.Next != null) 
            {
                previous = currentNode;
                currentNode = currentNode.Next;

                currentNode.Previous = previous;
            }
        }

        public void PrintStudents() 
        {
            currentNode = header;

            int count = 1;

            if(header == null) 
            {
                Console.WriteLine("Список студентов пуст!");
                return;
            }

            while(currentNode.Next != null) 
            {
                Console.WriteLine($"[{count++}] {currentNode.FullName}\tТелефон: {currentNode.Phone}\tДень рождения: {currentNode.BirthDate.ToLongDateString()}");

                currentNode = currentNode.Next;

                if(currentNode.Next == null) 
                {   
                    Console.WriteLine($"[{count++}] {currentNode.FullName}\tТелефон: {currentNode.Phone}\tДень рождения: {currentNode.BirthDate.ToLongDateString()}");
                    break;
                }
            }
        }

        public void Remove(string name) 
        {
            currentNode = header;
            DoublyLinkedList next;

            while(currentNode.Next != null) 
            {
                if(name == currentNode.FullName) 
                {   
                    if(currentNode.Previous == null) {
                        header = currentNode.Next;
                        break;
                    }
                    previous = currentNode.Previous;
                    next = currentNode.Next;

                    previous.Next = next;
                    next.Previous = previous;

                    break;
                }
                currentNode = currentNode.Next;
            }

            if (currentNode.Next == null && name == currentNode.FullName) 
            {
                previous = currentNode.Previous;
                previous.Next = null;
            }
        }

        public void Sorting()
        {
            currentNode = header;

            DoublyLinkedList temp = header;

            Dictionary<string, DateTime> dict = new Dictionary<string, DateTime>();

            while (temp != null) 
            {
                dict.Add(temp.FullName, temp.BirthDate);
                temp = temp.Next;
            }

            var sortedPeople = dict.OrderBy(kvp => kvp.Value);
            dict = sortedPeople.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            string[] keysDict = dict.Keys.Cast<string>().ToArray(); //Преобразование всех элементов(ключей) словаря в IEnumerable<T>,
                                                                    //затем происходит копирование всех элементов за счёт метода .ToArray() в массив


            for (int i = 0; i < keysDict.Length; i++)
            {
                currentNode.FullName = keysDict[i];
                currentNode.BirthDate = dict[keysDict[i]];

                currentNode = currentNode.Next;  
            }
        }
    }
}

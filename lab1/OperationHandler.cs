using System;

namespace lab1
{
    class OperationHandler
    {  
        private StudentsGroup StudentsGroup { get; }

        public OperationHandler() 
        {
            StudentsGroup studentsGroup = new StudentsGroup();
            StudentsGroup = studentsGroup;

            studentsGroup.Add("Чистяков Даниил Сергеевич", "+7(952)510-29-69", new DateTime(2004, 09, 06));
            studentsGroup.Add("Попов Артём Алексеевич", "+7(902)311-37-59", new DateTime(2003, 07, 01));
            studentsGroup.Add("Смиронов Егор Иванович", "+7(914)735-97-81", new DateTime(2003, 04, 17));
            studentsGroup.Add("Лебедев Пётр Михайлович", "+7(974)235-17-61", new DateTime(2000, 08, 22));
            studentsGroup.Add("Баранов Николай Романович", "+7(924)515-67-61", new DateTime(2002, 03, 17));
            studentsGroup.Add("Порохин Максим Андреевич", "+7(953)121-77-11", new DateTime(2002, 10, 24));
            studentsGroup.Add("Медведев Артём Иванович", "+7(919)615-87-19", new DateTime(2005, 07, 11));
        }

        public void Handler() 
        {
            DateTime dateTime = new DateTime();

            string fullName, phone;
            char operation = ' ';
            
            Console.WriteLine("===========Доступные операции=============\n1 - Вывести всех студентов.\n" +
                "2 - Удалить студента из списка.\n3 - Добавить студента в список.\n4 - Отсортировать студентов по ДД.ММ.ГГГГ" +
                "\n==========================================");

            while (operation != '0') 
            {
                Console.Write("\nВведите номер операции (5 - вывод дос. операций)\n");
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write("---> ");
                Console.ForegroundColor = ConsoleColor.White;

                operation = (char)Console.ReadKey().Key;
                Console.WriteLine();

                switch (operation)
                {
                    case ('1'):

                        StudentsGroup.PrintStudents();
                        break;

                    case ('2'):
                        try 
                        {
                            Console.Write("Введите ФМО студента, которого хотите удалить: ");
                        
                            StudentsGroup.Remove(Console.ReadLine());
                        }
                        catch(NullReferenceException ex){
                            Console.WriteLine("Список студентов пустой!");
                        }

                        break;

                    case ('3'):
                        try 
                        { 
                            Console.Write("Введите ФМО студента: ");
                            fullName = Console.ReadLine();

                            Console.WriteLine();

                            Console.Write("Введите телефон студента: ");
                            phone = Console.ReadLine();
                            Console.WriteLine();

                            Console.Write("Введите ГГГГ.ММ.ДД студента: ");
                            dateTime = DateTime.Parse(Console.ReadLine());
                            
                            StudentsGroup.Add(fullName, phone, dateTime);
                        }
                        catch {
                            Console.Clear();
                            Console.WriteLine("Неверный формат ГГГГ.ММ.ДД!");
                        }
                        
                        break;

                    case ('4'):
                        StudentsGroup.Sorting();
                        StudentsGroup.PrintStudents();
                        break;

                    case ('5'):
                        Console.WriteLine("\n===========Доступные операции=============\n1 - Вывести всех студентов.\n" +
                        "2 - Удалить студента из списка.\n3 - Добавить студента в список.\n4 - Отсортировать студентов по ДД.ММ.ГГГГ" +
                        "\n==========================================");

                        break;
                }
            }
        }
    }
}

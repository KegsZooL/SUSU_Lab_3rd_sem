using lab_2;

namespace Program
{
    class Program
    {
        static void Main(string[] args) 
        {
            OperationHandler operationHandler = new OperationHandler(); /* Создаю экземпляр класса OperationHandler
                                                                         * и вызываю метод Start() для обработки операций над множествами.
                                                                        */
            operationHandler.Start();
        }
    }
}

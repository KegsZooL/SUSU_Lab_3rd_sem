using lab3;

namespace Program
{
    class Program
    {
        static void Main()
        {
            GraphicEditor graphicEditor = new GraphicEditor();

            graphicEditor.Add(new Rectangle (frameThickness: 3, width: 10, height: 5)  );
            graphicEditor.Add(new Square    (frameThickness: 5, width: 15, height: 15) );
            graphicEditor.Add(new Ellipse   (frameThickness: 3, width: 5,  height: 7)  );
            graphicEditor.Add(new Circle    (frameThickness: 2, radius: 10)            );

            OperationHandler operationHandler = new OperationHandler();
            operationHandler.Menu();
        }
    }
}

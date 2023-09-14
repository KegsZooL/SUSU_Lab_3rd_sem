using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace lab3
{
    class FilesHandler
    {   
        public static void ToJson(string pathToFile, Object objects)
        {
            char status;

            FileStream fileStream;

            if(File.Exists(pathToFile) == false) 
            {
                Console.Write($"\u001b[31m\n   Файл ({pathToFile}) не существует!\u001b[0m --> " +
                    $"Хотите создать .json файл?(y/n): ");

                status = Console.ReadLine()[0];

                if (status == 'y') 
                {
                    try 
                    {
                        using (fileStream = File.Create(pathToFile)) 
                        {
                            Console.WriteLine($"\n   Файл создан и находится по пути: {pathToFile}\n");
                        };
                    }

                    catch(ArgumentException) // Обработка некорректного имени файла
                    {
                        string projectDirectory = Directory.GetCurrentDirectory();

                        pathToFile = $"{projectDirectory}\\MyGraphicEditor.json";

                        using (fileStream = File.Create(pathToFile)) 
                        {
                            Console.WriteLine($"\n   Файл создан и находится по пути: {pathToFile}\n");
                        }

                        ToJson(pathToFile, objects);
                    }
                }
                else
                    return;
            }

            File.WriteAllText(pathToFile, JsonConvert.SerializeObject(
                objects, Formatting.Indented, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    }));
        }

        public static GraphicEditor FromJson(string pathToFile) 
        {
            GraphicEditor graphicEditor = new GraphicEditor();

            try 
            {
                var figures = JsonConvert.DeserializeObject<List<Figure>>(
                    File.ReadAllText(pathToFile), new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });

                if (figures != null)
                    graphicEditor.listFigures.AddRange(figures);
            }
            
            catch(DirectoryNotFoundException) {
                Console.WriteLine($"\u001b[31m\n   Файл ({pathToFile}) не существует!\u001b[0m\n");
            }

            return graphicEditor;
        }
    }
}

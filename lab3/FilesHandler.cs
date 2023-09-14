using Newtonsoft.Json; // JSON Framework https://www.newtonsoft.com/json
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

            // Определяем текущий формат файла для дальнейшей проверки, что файл имеет формат .json
            string currentFormatFile = Path.GetExtension(pathToFile);

            if(File.Exists(pathToFile) == false) // Проверка на существование файла в директории
            {
                Console.Write($"\u001b[31m\n   Файл ({pathToFile}) не существует!\u001b[0m --> " +
                    $"Хотите создать .json файл?(y/n): ");

                status = Console.ReadLine()[0];

                if (status == 'y') 
                {   
                    if(currentFormatFile == ".json") // Если исходный путь к файлу имеет формат .json
                    { 
                        using (fileStream = File.Create(pathToFile))
                        {
                            Console.WriteLine($"\n   Файл создан и находится по пути: {pathToFile}\n");
                        };
                    }
                    else
                    {
                        string currentProjectDirectory = Directory.GetCurrentDirectory();

                        pathToFile = $"{currentProjectDirectory}\\MyGraphicEditor.json";

                        using (fileStream = File.Create(pathToFile))
                        {
                            Console.WriteLine($"\n   Файл создан и находится по пути: {pathToFile}\n");
                        }

                        /* Если исходный путь к файлу не имеет формат .json, то создаем файл в директории проекта
                         * и передаем новый путь через рекурсию
                        */
                        ToJson(pathToFile, objects); 
                    }
                }
                else
                    return; // Выход из функции при отклонении создания файла
            }

            // Сохранение объектов в файл JSON с форматированием и включением информации о типах
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
            {   // Десериализация данных из файла и добавление в список объкта GraphicEditor
                var figures = JsonConvert.DeserializeObject<List<Figure>>(
                    File.ReadAllText(pathToFile), new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });

                if (figures != null)
                    graphicEditor.listFigures.AddRange(figures);
            }

            // Обработка исключения (файл не найден)
            catch (FileNotFoundException) { 
                Console.WriteLine($"\u001b[31m\n   Файл ({pathToFile}) не существует!\u001b[0m\n");
            }

            catch(Exception ex) {
                Console.WriteLine($"Произошла ошибка при загрузки данных: {ex.Message}");
            }

            return graphicEditor;
        }
    }
}
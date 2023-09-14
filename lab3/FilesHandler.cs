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
            File.WriteAllText(pathToFile, JsonConvert.SerializeObject(
                objects, Formatting.Indented, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    }));
        }

        public static GraphicEditor FromJson(string pathToFile) 
        {
            GraphicEditor graphicEditor = new GraphicEditor();

            var figures = JsonConvert.DeserializeObject<List<Figure>>(
                File.ReadAllText(pathToFile), new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (figures != null)
                graphicEditor.listFigures.AddRange(figures);

            return graphicEditor;
        }
    }
}

using System;
using System.Collections.Generic;

namespace lab4
{
    class HrefHandler : IHandler
    {   
        int MaxNumberOfPages { get; set; }

        int MaxDepth { get; set; }

        string BaseCatalogURI { get; set; }

        //Коллекция пройденных ссылок
        readonly HashSet<string> passedLinks = new HashSet<string>();

        /*
         * В конструкторе опредяляем для свойств значения максимальное количество странниц
         * и максимальную вложенность
        */

        public HrefHandler(int maxNumberOfPages, int maxDepth) 
        {   
            MaxNumberOfPages = maxNumberOfPages;
            
            MaxDepth = maxDepth;
        } 
        
        public void Process(Uri uri, int currentDepth) 
        {   
            if (MaxNumberOfPages <= 0)
                return;
            
            //Определяем базовый каталог страницы
            if(BaseCatalogURI == null) 
                BaseCatalogURI = uri.ToString();
            
            //Добавляем пройденную страницу в коллекцию
            passedLinks.Add(uri.ToString());
            --MaxNumberOfPages;
            
            //Получаем html код текущей страницы
            string currentPage = Utils.GetPageByURI(uri);

            //Получаем все возможные ссылки на страницы
            List<string> parametrsURI = Utils.GetAllLinks(ref currentPage);

            if (parametrsURI[0] == "#main-content")
                parametrsURI.Remove(parametrsURI[0]);

            //Рекурсия на основе вызова события с проверкой текущей вложенности
            while (passedLinks.Count <= MaxNumberOfPages)
            {
                try 
                {
                    foreach (var link in parametrsURI)
                    {   
                        if (!passedLinks.Contains(link) && (link.ToString().StartsWith(BaseCatalogURI) && currentDepth <= MaxDepth))
                        {
                            RequestEvent.Notify(new Uri(link), currentDepth++);
                        }
                    }
                }
                catch 
                {
                    continue;
                }
            }
        }
    }
}
using System.Collections.Generic;
using System.Text.RegularExpressions;

//класс для события
//!нет, это класс парсера, который из строки делает объект
namespace WpfApplication2
{
    public class Parser
    {
        public Log Log_Parse(string str) //если сообщение пришло то выводим его
        {
            string pattern = "	";
            Regex r = new Regex(pattern); //разделяшки
            List<string> words = new List<string>(r.Split(str)); //пока разделяем, записываем в лист words

            // Создаем объект класса Log
            // ! используй такую инициализацию объектов
            Log logSecurity = new Log
            {
                System = words[0],
                User = words[1],
                GuId = int.Parse(words[4]),
                EventID = int.Parse(words[6]),
                Domain = words[11],
                date = words[5],
                other = words[14]
            };

            return logSecurity;
        }
    }
}

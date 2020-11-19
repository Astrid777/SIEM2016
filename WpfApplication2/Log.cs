using System;
using Finisar.SQLite;

namespace WpfApplication2
{
    public class Log
    {
        // Поля класса
        //! в C# публичные поля используются крайне редко. Вместо них используются свойства. Используй только их!
        public string System { get; set; }
        public string User { get; set; }
        public int GuId { get; set; }
        public int EventID { get; set; }
        public string Domain { get; set; }
        public string date { get; set; }
        public string other { get; set; }

        // Метод, выводящий в консоль информацию
        // ! событие не должно себя в консоль выводить. Надеюсь, ты прочитала про принцип единственности ответствености
        // ! для того, чтобы выводить информацию об объекте в виде строки используется метод ToString. 
        // ! ToString вызывается в ConsoleWriteline когда ты в качестве параметра туда объект передаешь
        // ! кроме того, его удобно использовать при отладке. Когда во время отладки ты наведешь мышкой на экземпляре класса, 
        // ! то в описании вызовется метод ToString
        public override string ToString()
        {
            return "System: " + System + "\n"
                   + "User: " + User + "\n"
                   + "Guid: " + GuId + "\n"
                   + "CodeEvent" + EventID + "\n"
                   + "Domain" + Domain + "\n"
                   + "date" + date + "\n"
                   + "other" + other + "\n";
        }
    }
}

using System.Net.Sockets;

namespace WpfApplication2
{
    public class StateObject //класс для хранения принятых байт
    {
        public byte[] buffer; //массив принимаемых байт
        public int BufferSize = 4096; //максимальный размер буфера
        public Socket workSocket; //переменная

        public StateObject() //метод
        {
            buffer = new byte[BufferSize];
        }
    }
}
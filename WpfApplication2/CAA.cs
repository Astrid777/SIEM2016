using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WpfApplication2
{
    internal class CAA
    {
        private readonly IPAddress Target_IP;
        private readonly int Target_Port;

        private readonly Socket UDPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); //конструктор
                                                                                                                        //протокол передачи - UDP,                                                                                                                             //AddressFamily.InterNetwork - возвращаем адреса, исп-ые сокетом, IPv4                                                                                                                     //SocketType.Dgram - получаем датаграммы по UDP
        public Queue<string> MessageQueue { get; private set; }

        public CAA(string ipOfYourPC, int portNumber)
        {
            Target_IP = IPAddress.Parse(ipOfYourPC);
            Target_Port = portNumber;
            MessageQueue = new Queue<string>();
            try
            {
                var localHostIpEnd = new IPEndPoint(IPAddress.Any, Target_Port);
                UDPSocket.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.NoDelay, 1);
                UDPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                UDPSocket.Bind(localHostIpEnd);
                UDPSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 0);
              //  Console.WriteLine("Starting Recieve");
                Recieve();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Recieve()
        {
            try
            {
                var LocalIPEndPoint = new IPEndPoint(IPAddress.Any, Target_Port);
                EndPoint LocalEndPoint = LocalIPEndPoint;
                StateObject state = new StateObject();
                state.workSocket = UDPSocket;
               // Console.WriteLine("Begin Recieve");
               // Console.WriteLine("-------------");
                UDPSocket.BeginReceiveFrom(state.buffer, 0, state.BufferSize, 0, ref LocalEndPoint, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            var LocalIPEndPoint = new IPEndPoint(IPAddress.Any, Target_Port);
            EndPoint LocalEndPoint = LocalIPEndPoint;
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            var bytesRead = client.EndReceiveFrom(ar, ref LocalEndPoint);

            client.BeginReceiveFrom(state.buffer, 0, state.BufferSize, 0, ref LocalEndPoint, new AsyncCallback(ReceiveCallback), state);

            string data = Encoding.Default.GetString(state.buffer, 0, state.BufferSize).Replace("\0", "");

            MessageQueue.Enqueue(data);
        }
    }
}
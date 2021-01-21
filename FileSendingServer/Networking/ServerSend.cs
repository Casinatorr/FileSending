using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSendingServer.Networking
{
    class ServerSend
    {
        private static void SendTCPData(int toClient, Packet p)
        {
            p.WriteLength();
            Server.clients[toClient].tcp.SendData(p);
        }
        
        private static void SendTCPDataToAll(Packet p)
        {
            p.WriteLength();
            foreach (Client c in Server.clients.Values.ToList())
                c.tcp.SendData(p);
        }

        private static void SendTCPDataToAll(int except, Packet p)
        {
            p.WriteLength();
            foreach (Client c in Server.clients.Values.ToList())
            {
                if (c.id == except)
                    continue;
                c.tcp.SendData(p);
            }
        }

        public static void SendFileNotification(string fileName, int size)
        {
            using (Packet p = new Packet((int) ServerPackets.sendFileNotifiaction))
            {
                p.Write(size);
                p.Write(fileName);

                SendTCPDataToAll(p);
            }
        }
    }
}

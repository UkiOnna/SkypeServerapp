using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SkypeServerApp
{
 public   class Server
    {
        public async void Working(TcpListener server)
        {
            while (true)
            {
                //Socket incomingSocket = server.AcceptSocket();
                //дальше работаем как с сокетом
                //TcpClient client = server.AcceptTcpClient();
                TcpClient client = await server.AcceptTcpClientAsync();

                int bytes;
                byte[] buffer = new byte[1024];
                StringBuilder strBulder = new StringBuilder();

                using (var networkStream = client.GetStream())
                {
                    // do
                    //{
                    while (networkStream.DataAvailable)
                    {
                        bytes = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                        strBulder.Append(Encoding.Default.GetString(buffer));
                        FileInfo file = JsonConvert.DeserializeObject<FileInfo>(strBulder.ToString());
                        File.WriteAllBytes(Directory.GetCurrentDirectory() + "/aqwa" + "/" + file.fileName, file.Bytes);
                        Console.WriteLine("Upload!");
                    }
                    //if (networkStream.DataAvailable)
                    //{
                        
                    //}

                    //  }

                }
                Console.WriteLine(strBulder);
                client.Close();
            }
        }
    }
}

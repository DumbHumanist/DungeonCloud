using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class NetworkManager
    {
        static public void StartListen()
        {
            string ip = "127.0.0.1";
            int port = 23737;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            server.Bind(ep);
            server.Listen(5);
            Console.WriteLine("Server started...");
            while (true)
            {
                Socket client = server.Accept();
                Console.WriteLine($"{DateTime.Now} connected {client.RemoteEndPoint}");
                Task.Run(() => HandleClient(client));
            }

            void HandleClient(Socket client)
            {
                byte[] recievedBytes = new byte[10240];
                client.Receive(recievedBytes);
                string request = Encoding.Default.GetString(recievedBytes);
                Package reqPackage = JsonConvert.DeserializeObject<Package>(request);


                if (reqPackage.Type==0)
                {            
                   
                    using (UserDirectoryContext db = new UserDirectoryContext())
                    {
                        foreach(var i in db.UserDirectories)
                        {
                            if(i.UserSub.Contains(reqPackage.Sub) || reqPackage.Sub.Contains(i.UserSub))
                            {
                                client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(i)}"));
                                break;
                            }
                        }
                        UserDirectory tmp = new UserDirectory();
                        tmp.UserSub = reqPackage.Sub;
                        tmp.Dir = new System.IO.DirectoryInfo($"{reqPackage.Sub}");
                        db.UserDirectories.AddOrUpdate(tmp);
                        db.SaveChanges();
                        client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(tmp)}"));
                    }
                }
                else if (reqPackage.Type == 1)
                {
                    using (UserDirectoryContext db = new UserDirectoryContext())
                    {
                        using (var stream = new NetworkStream(client))
                        using (var output = File.Create($"{reqPackage.Path}\\{reqPackage.FileTransfer.Name}"))
                        {
                            var buffer = new byte[(reqPackage.FileTransfer as FileInfo).Length];
                            int bytesRead;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                output.Write(buffer, 0, bytesRead);
                            }
                        }
                        UserDirectory tmp = new UserDirectory();
                        tmp.UserSub = reqPackage.Sub;
                        tmp.Dir = new System.IO.DirectoryInfo($"{reqPackage.UserDirectory.Dir.Name}");
                        db.UserDirectories.AddOrUpdate(tmp);
                        db.SaveChanges();
                        client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(tmp)}"));
                    }             
                }
                else if (reqPackage.Type == 3)
                {
                    using (UserDirectoryContext db = new UserDirectoryContext())
                    {

                        File.Delete($"{reqPackage.UserDirectory.Dir.Name}\\{reqPackage.Path}");

                        UserDirectory tmp = new UserDirectory();
                        tmp.UserSub = reqPackage.Sub;
                        tmp.Dir = new System.IO.DirectoryInfo($"{reqPackage.UserDirectory.Dir.Name}");
                        db.UserDirectories.AddOrUpdate(tmp);
                        db.SaveChanges();
                        client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(tmp)}"));
                    }
                }
                else if(reqPackage.Type == 2)
                {
                    client.SendFile($"{reqPackage.UserDirectory.Dir.Name}\\{reqPackage.Path}");
                }

                client.Shutdown(SocketShutdown.Both);
                client.Dispose();
            }
        }
    }
}

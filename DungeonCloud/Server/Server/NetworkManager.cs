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

            DungeonDirectoryInfo GetUserDirectory(string path)
            {
                DungeonDirectoryInfo dungeonDirectoryInfoTemp = new DungeonDirectoryInfo();
                dungeonDirectoryInfoTemp.Path = path;
                dungeonDirectoryInfoTemp.Name = path.Split('\\')[path.Split('\\').Length - 1];
                foreach (var j in new DirectoryInfo(path).GetFiles())
                {
                    DungeonFileInfo fi = new DungeonFileInfo();
                    fi.FileSize = j.Length;
                    fi.Name = j.Name;
                    fi.Path = path + "\\" + j.Name;
                    dungeonDirectoryInfoTemp.ChildrenFiles.Add(fi);
                }
                foreach (var s in new DirectoryInfo(path).GetDirectories())
                {
                    dungeonDirectoryInfoTemp.ChildrenFolders.Add(GetUserDirectory(path+"\\"+s.Name));
                }

                return dungeonDirectoryInfoTemp;
            }

            void HandleClient(Socket client)
            {
                byte[] recievedBytes = new byte[10240];
                client.Receive(recievedBytes);
                string request = Encoding.Default.GetString(recievedBytes);
                Package reqPackage = JsonConvert.DeserializeObject<Package>(request);


                if (reqPackage.Type == 0)
                {

                    using (UserDirectoryContext db = new UserDirectoryContext())
                    {
                        bool flag = true;
                        foreach(var i in db.UserDirectories)
                        {
                            if (i.UserSub.Contains(reqPackage.Sub) || reqPackage.Sub.Contains(i.UserSub))
                            {
                                UserDirectory tmp = new UserDirectory();
                                tmp.UserSub = reqPackage.Sub;
                                tmp.Dir = GetUserDirectory(reqPackage.Sub);
                                client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(tmp)}")); 
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            UserDirectory tmp = new UserDirectory();
                            tmp.UserSub = reqPackage.Sub;
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "//" + reqPackage.Sub);
                            tmp.Dir = GetUserDirectory(reqPackage.Sub);
                            db.UserDirectories.AddOrUpdate(tmp);
                            db.SaveChanges();
                            client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(tmp)}"));
                        }
                    }
                }
                else if (reqPackage.Type == 1)
                {
                    using (UserDirectoryContext db = new UserDirectoryContext())
                    {
                        using (var stream = new NetworkStream(client))
                        using (var output = File.Create($"{reqPackage.Path}\\{reqPackage.FileTransfer.Name}"))
                        {
                            var buffer = new byte[reqPackage.FileTransfer.FileSize];
                            int bytesRead;
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                output.Write(buffer, 0, bytesRead);
                            }
                        }
                        UserDirectory tmp = new UserDirectory();
                        tmp.UserSub = reqPackage.Sub;
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "//" + reqPackage.Sub);
                        tmp.Dir = GetUserDirectory(reqPackage.Sub);
                        db.UserDirectories.AddOrUpdate(tmp);
                        db.SaveChanges();
                        client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(tmp)}"));
                    }
                }
                else if (reqPackage.Type == 3)
                {
                    using (UserDirectoryContext db = new UserDirectoryContext())
                    {

                        File.Delete($"{reqPackage.Sub}\\{reqPackage.Path}");

                        UserDirectory tmp = new UserDirectory();
                        tmp.UserSub = reqPackage.Sub;
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "//" + reqPackage.Sub);
                        tmp.Dir = GetUserDirectory(reqPackage.Sub);
                        db.UserDirectories.AddOrUpdate(tmp);
                        db.SaveChanges();
                        client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(tmp)}"));
                    }
                }
                else if (reqPackage.Type == 2)
                {
                    string str = $"{reqPackage.UserDirectory.UserSub}\\{reqPackage.Path}";
                    client.SendFile(str);
                }

                client.Shutdown(SocketShutdown.Both);
                client.Dispose();
            }
        }
    }
}

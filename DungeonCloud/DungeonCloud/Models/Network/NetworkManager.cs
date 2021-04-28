using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DungeonCloud.Infrastructure;
using System.Diagnostics.Eventing.Reader;
using DungeonCloud.Models.Files;

namespace DungeonCloud.Models.Network
{
    class NetworkManager : PropertyChangedBase
    {

        private bool sessionStarted = false;
        public bool SessionStarted
        {
            get => sessionStarted;
            set
            {
                sessionStarted = value;
                NotifyOfPropertyChange();
            }
        }

        private User session;
        public User Session
        {
            get => session;
            set
            {
                session = value;
                NotifyOfPropertyChange();
            }
        }
        public UserDirectory LoadFiles(User Session)
        {
            try
            {
                string serverIP = "127.0.0.1";
                int serverPort = 23737;
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                client.Connect(ep);
                client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(new Package(0, this.Session))}"));
                byte[] bytes = new byte[500000];
                client.Receive(bytes);
                string data = Encoding.Default.GetString(bytes);
                return JsonConvert.DeserializeObject<UserDirectory>(data);
            }
            catch
            {
                return null;
            }
        }

        public UserDirectory UploadNewFile(UserDirectory userDirectory, DungeonFileInfo fileToUpload,string pathToFileFromUserDirectory, string localPath)
        {
            string serverIP = "127.0.0.1";
            int serverPort = 23737;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            client.Connect(ep);
            client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(new Package(1, userDirectory, this.Session, fileToUpload, pathToFileFromUserDirectory))}"));
            client.SendFile(localPath);
            byte[] bytes = new byte[500000];
            client.Receive(bytes);
            string data = Encoding.Default.GetString(bytes);
            return JsonConvert.DeserializeObject<UserDirectory>(data);
        }

        public byte[] DownloadFile(UserDirectory userDirectory, string pathToFileFromUserDirectory, int fileSize)
        {
            string serverIP = "127.0.0.1";
            int serverPort = 23737;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            client.Connect(ep);
            client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(new Package(2,userDirectory,pathToFileFromUserDirectory))}"));
            byte[] buf = new byte[fileSize];
            client.Receive(buf);
            return buf;          
        }

        public UserDirectory RemoveFile(UserDirectory userDirectory, string pathToFileFromUserDirectory)
        {
            string serverIP = "127.0.0.1";
            int serverPort = 23737;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            client.Connect(ep);
            client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(new Package(3, userDirectory, pathToFileFromUserDirectory))}"));
            byte[] bytes = new byte[500000];
            client.Receive(bytes);
            string data = Encoding.Default.GetString(bytes);
            return JsonConvert.DeserializeObject<UserDirectory>(data);
        }

        public UserDirectory CreateNewFolder(UserDirectory userDirectory, string pathToFileFromUserDirectory)
        {
            string serverIP = "127.0.0.1";
            int serverPort = 23737;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            client.Connect(ep);
            client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(new Package(4, userDirectory, pathToFileFromUserDirectory))}"));
            byte[] buf = new byte[500000];
            client.Receive(buf);
            string data = Encoding.Default.GetString(buf);
            return JsonConvert.DeserializeObject<UserDirectory>(data);
        }
        public UserDirectory DeleteFilder(UserDirectory userDirectory, string pathToFileFromUserDirectory)
        {
            string serverIP = "127.0.0.1";
            int serverPort = 23737;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            client.Connect(ep);
            client.Send(Encoding.Default.GetBytes($"{JsonConvert.SerializeObject(new Package(5, userDirectory, pathToFileFromUserDirectory))}"));
            byte[] buf = new byte[500000];
            client.Receive(buf);
            string data = Encoding.Default.GetString(buf);
            return JsonConvert.DeserializeObject<UserDirectory>(data);
        }


        const string clientId = "737979910974-gmk92cjm195r8mjk1i83tajo1ue5j4f6.apps.googleusercontent.com";
            const string clientSecret = "rtAwrlF73T_vzt5V5aRc8f8s";

            

            const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";

            public NetworkManager()
            {

            }

        

        private static int GetRandomUnusedPort()
            {
                var listener = new TcpListener(IPAddress.Loopback, 0);
                listener.Start();
                var port = ((IPEndPoint)listener.LocalEndpoint).Port;
                listener.Stop();
                return port;
            }

            private async Task DoOAuthAsync(string clientId, string clientSecret)
            {
                string state = GenerateRandomDataBase64url(32);
                string codeVerifier = GenerateRandomDataBase64url(32);
                string codeChallenge = Base64UrlEncodeNoPadding(Sha256Ascii(codeVerifier));
                const string codeChallengeMethod = "S256";

                string redirectUri = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";
                var http = new HttpListener();
                http.Prefixes.Add(redirectUri);
                http.Start();
                string authorizationRequest = string.Format("{0}?response_type=code&scope=openid%20profile&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}", AuthorizationEndpoint, Uri.EscapeDataString(redirectUri), clientId, state, codeChallenge, codeChallengeMethod);
                Process.Start(authorizationRequest);

                var context = await http.GetContextAsync();

                var response = context.Response;
                string responseString = "<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Auth complete</body></html>";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                var responseOutput = response.OutputStream;
                await responseOutput.WriteAsync(buffer, 0, buffer.Length);
                responseOutput.Close();
                http.Stop();

                var code = context.Request.QueryString.Get("code");
                var incomingState = context.Request.QueryString.Get("state");

                await ExchangeCodeForTokensAsync(code, codeVerifier, redirectUri, clientId, clientSecret);
            }

            async Task ExchangeCodeForTokensAsync(string code, string codeVerifier, string redirectUri, string clientId, string clientSecret)
            {
                string tokenRequestUri = "https://www.googleapis.com/oauth2/v4/token";
                string tokenRequestBody = string.Format("code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}&client_secret={4}&scope=&grant_type=authorization_code", code, Uri.EscapeDataString(redirectUri), clientId, codeVerifier, clientSecret);

                HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestUri);
                tokenRequest.Method = "POST";
                tokenRequest.ContentType = "application/x-www-form-urlencoded";
                tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                byte[] tokenRequestBodyBytes = Encoding.ASCII.GetBytes(tokenRequestBody);
                tokenRequest.ContentLength = tokenRequestBodyBytes.Length;
                using (Stream requestStream = tokenRequest.GetRequestStream())
                {
                    await requestStream.WriteAsync(tokenRequestBodyBytes, 0, tokenRequestBodyBytes.Length);
                }

                try
                {
                    WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
                    using (StreamReader reader = new StreamReader(tokenResponse.GetResponseStream()))
                    {
                        string responseText = await reader.ReadToEndAsync();
                        Console.WriteLine(responseText);

                        Dictionary<string, string> tokenEndpointDecoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);
                        string accessToken = tokenEndpointDecoded["access_token"];
                        await RequestUserInfoAsync(accessToken);
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        var response = ex.Response as HttpWebResponse;
                        if (response != null)
                        {
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                string responseText = await reader.ReadToEndAsync();
                            }
                        }

                    }
                }
            }

            private async Task RequestUserInfoAsync(string accessToken)
            {
                string userinfoRequestUri = "https://www.googleapis.com/oauth2/v3/userinfo";

                HttpWebRequest userinfoRequest = (HttpWebRequest)WebRequest.Create(userinfoRequestUri);
                userinfoRequest.Method = "GET";
                userinfoRequest.Headers.Add(string.Format("Authorization: Bearer {0}", accessToken));
                userinfoRequest.ContentType = "application/x-www-form-urlencoded";
                userinfoRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

                WebResponse userinfoResponse = await userinfoRequest.GetResponseAsync();
                using (StreamReader userinfoResponseReader = new StreamReader(userinfoResponse.GetResponseStream()))
                {
                    string userinfoResponseText = await userinfoResponseReader.ReadToEndAsync();
                    Session = JsonConvert.DeserializeObject<User>(userinfoResponseText);
                }
            }

            public async Task<int> StartAuthViaGoogle()
            {
                await DoOAuthAsync(clientId, clientSecret);
                return 0;
            }

            private static string GenerateRandomDataBase64url(uint length)
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] bytes = new byte[length];
                rng.GetBytes(bytes);
                return Base64UrlEncodeNoPadding(bytes);
            }

            private static byte[] Sha256Ascii(string text)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(text);
                using (SHA256Managed sha256 = new SHA256Managed())
                {
                    return sha256.ComputeHash(bytes);
                }
            }

            private static string Base64UrlEncodeNoPadding(byte[] buffer)
            {
                string base64 = Convert.ToBase64String(buffer);

                base64 = base64.Replace("+", "-");
                base64 = base64.Replace("/", "_");
                base64 = base64.Replace("=", "");

                return base64;
            }

            

        
    }
}

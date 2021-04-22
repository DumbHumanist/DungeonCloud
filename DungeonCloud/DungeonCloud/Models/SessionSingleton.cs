using DungeonCloud.Models.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCloud.Models
{
    class SessionSingleton
    {
        static SessionSingleton instance;

        NetworkManager networkManager = new NetworkManager();

        private SessionSingleton()
        {
            LoadSession();
        }

        public static SessionSingleton Instance => instance ?? (instance = new SessionSingleton());

        public async void LoadSession()
        {
            await networkManager.StartAuthViaGoogle();
        }
    }
}

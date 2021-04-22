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

        private NetworkManager nM;

        public NetworkManager NM
        {
            get => nM;
        }

        private SessionSingleton()
        {
            nM = new NetworkManager();
        }

        public static SessionSingleton Instance => instance ?? (instance = new SessionSingleton());
    }
}

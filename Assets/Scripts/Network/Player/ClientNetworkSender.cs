using System;
using Core.Player;

namespace Network.Player
{
    public class ClientNetworkSender : IDisposable
    {
        public ClientNetworkSender(IServerClientModelQuery model, INetworkClient networkClient)
        {
            networkClient.SyncId = model.Id;
        }

        public void Dispose()
        {
        }
    }
}
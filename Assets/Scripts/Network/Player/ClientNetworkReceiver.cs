using Core.Player;

namespace Network.Player
{
    public class ClientNetworkReceiver : IServerClientModelQuery
    {
        public ClientNetworkReceiver(INetworkClient networkClient)
        {
            Id = networkClient.SyncId;
        }

        public int Id { get; }
    }
}
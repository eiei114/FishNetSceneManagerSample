using FishNet.Connection;
using FishNet.Object;
using FishNet.Utility.Extension;
using Network.Player;
using UnityEngine;

namespace Factory
{
    public class ClientsFactory : MonoBehaviour
    {
        [SerializeField] private NetworkObject _clientPrefab;
        
        [Server]
        public NetworkClient CreateClient(NetworkConnection conn)
        {
            var client = Instantiate(_clientPrefab);
            client.Spawn(conn);
            return client.GetComponent<NetworkClient>();
        }
    }
}
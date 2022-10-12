using Core.Player;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace Network.Player
{
    public class NetworkClient:NetworkBehaviour,INetworkClient,IServerClientModelCommand
    {
        private IServerClientModelMutable _serverClientModel;

        private void Awake()
        {
            _serverClientModel = GetComponent<IServerClientModelMutable>();
        }

        [Server]
        public void InitServer(int id)
        {
            _serverClientModel.SetId(id);

            new ClientNetworkSender(_serverClientModel, this);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            if (!IsServer)
            {
                ((MonoBehaviour)_serverClientModel).enabled = false;
            }

            new ClientNetworkReceiver(this);
        }

        [field:SyncVar]
        public int SyncId { get; set; }
    }
}
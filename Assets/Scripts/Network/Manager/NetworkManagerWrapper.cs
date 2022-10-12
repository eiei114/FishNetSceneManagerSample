using System;
using System.Collections;
using System.Collections.Generic;
using Factory;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Managing.Scened;
using FishNet.Transporting;
using FishNet.Utility;
using Network.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Network
{
    //NetworkManagerをラップしたクラス
    public class NetworkManagerWrapper : MonoBehaviour
    {
        [SerializeField] private NetworkManager _networkManager;

        private static NetworkManagerWrapper _instance;

        public static NetworkManagerWrapper Instance => _instance;

        [SerializeField, Scene] private string _firstScene;
        [SerializeField, Scene] private string _secondScene;
        [SerializeField, Scene] private string _thirdScene;
        [SerializeField] private ReplaceOption _replaceOption = ReplaceOption.All;

        [SerializeField] private int _clientsCount = 0;
        [SerializeField] private int _maxClientsCount = 2;
        
        [SerializeField] private ClientsFactory _clientsFactory;
        private readonly PlayerIdAllocator _idAllocator = new PlayerIdAllocator();
        [SerializeField] private List<NetworkClient> _clients = new List<NetworkClient>();

        private void Start()
        {
            _networkManager.ServerManager.OnRemoteConnectionState += OnRemoteConnectionState;
            _networkManager.ClientManager.OnClientConnectionState += OnClientConnectionState;
            _networkManager.SceneManager.OnClientLoadedStartScenes += OnClientLoadedStartScenes;
        }

        #region FishNetActions

        private void OnClientLoadedStartScenes(NetworkConnection conn, bool asServer)
        {
            Debug.Log($"Client loaded start scenes.");
            
            CreateClient(conn);
        }

        private void OnClientConnectionState(ClientConnectionStateArgs args)
        {
            Debug.Log($"Client connection state: {args.ConnectionState}");
        }

        private void OnRemoteConnectionState(NetworkConnection conn, RemoteConnectionStateArgs args)
        {
            switch (args.ConnectionState)
            {
                case RemoteConnectionState.Started:
                {
                    _clientsCount++;
                    if (_clientsCount == _maxClientsCount)
                    {
                        Debug.Log($"All clients connected. Loading {_secondScene}.");
                        MoveToSecondScene();
                        StartCoroutine(DelaySceneLoad());
                    }

                    break;
                }
                case RemoteConnectionState.Stopped:
                    _clientsCount--;
                    Debug.Log($"Client disconnected. {_clientsCount} clients remaining.");
                    MoveToFirstScene();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region ScnenChanges

        public void MoveToFirstScene()
        {
            SceneManager.LoadSceneAsync(_firstScene, LoadSceneMode.Single);
        }

        public void MoveToSecondScene()
        {
            var sld = new SceneLoadData(_secondScene) { ReplaceScenes = _replaceOption };
            _networkManager.SceneManager.LoadGlobalScenes(sld);
        }

        public void MoveToThirdScene()
        {
            Debug.Log($"Loading {_thirdScene}.");
            var sld = new SceneLoadData(_thirdScene) { ReplaceScenes = _replaceOption };
            _networkManager.SceneManager.LoadGlobalScenes(sld);
        }

        #endregion

        private IEnumerator DelaySceneLoad()
        {
            yield return new WaitForSeconds(5f);
            MoveToThirdScene();
        }
        
        private class PlayerIdAllocator
        {
            private int _current;

            public int Next() => _current++;
        }

        private void CreateClient(NetworkConnection conn)
        {
            var client = _clientsFactory.CreateClient(conn);
            var id= _idAllocator.Next();
            client.InitServer(id);
            
            _clients.Add(client);
        }
    }
}
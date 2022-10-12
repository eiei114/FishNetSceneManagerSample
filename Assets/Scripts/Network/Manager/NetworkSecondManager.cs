using System;
using System.Collections;
using FishNet.Object;
using UnityEngine;

namespace Network
{
    public class NetworkSecondManager : MonoBehaviour
    {
        private NetworkManagerWrapper _networkManagerWrapper;

        private void Start()
        {
            _networkManagerWrapper = NetworkManagerWrapper.Instance;
            Debug.Log($"SecondScene Start");
            SceneLoad();
        }

        [Server]
        private void SceneLoad()
        {
            StartCoroutine(DelaySceneLoad());
        }

        private IEnumerator DelaySceneLoad()
        {
            yield return new WaitForSeconds(5f);
            _networkManagerWrapper.MoveToThirdScene();
        }
    }
}
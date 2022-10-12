using UnityEngine;

namespace Core.Player
{
    public class ServerClient : MonoBehaviour,IServerClientModelMutable
    {
        [SerializeField] private int _id;
        public int Id => _id;
        public void SetId(int id)
        {
            _id = id;
        }
    }
}
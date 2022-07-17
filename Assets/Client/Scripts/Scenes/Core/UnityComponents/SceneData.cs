using UnityEngine;

namespace Kadoy.DoorTest.Core.Scene
{
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField]
        public Camera MainCamera { get; private set; }
        
        [field: SerializeField]
        public Transform PlayerSpawnPoint { get; private set; }
        
        [field: SerializeField]
        public Transform WorldRoot { get; private set; }
    }
}
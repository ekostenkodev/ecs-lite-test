using UnityEngine;

namespace Kadoy.DoorTest.Core.View
{
    public class DoorView : MonoBehaviour
    {
        [field:SerializeField]
        public string Id { get; private set; }
        
        [field:SerializeField]
        public Transform Root { get; private set; }

        [field:SerializeField]
        public float OpenSpeed { get; private set; }
        
        [field:SerializeField]
        public Vector3 CloseStatePosition { get; private set; }
    }
}
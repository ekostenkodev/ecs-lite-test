using UnityEngine;

namespace Kadoy.DoorTest.Core.View
{
    public class ActorView : MonoBehaviour
    {
        [field:SerializeField]
        public Transform Root { get; private set; }
        
        [field:SerializeField]
        public Animator Animator { get; private set; }
        
        [field:SerializeField]
        public float Radius { get; private set; }
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (Root != null)
            {
                UnityEditor.Handles.color = new Color(0, 1, 0, 0.2f);
                UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.up, Radius);
            }
        }
#endif
    }
}
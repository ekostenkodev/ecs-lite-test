using UnityEngine;

namespace Kadoy.DoorTest.Core.View
{
    public class CircleButtonView : MonoBehaviour
    {
        [field:SerializeField]
        public string InteractiveObjectId { get; private set; }
        
        [field:SerializeField]
        public float InteractRadius { get; private set; }
        
        [field:SerializeField]
        public Transform Root { get; private set; }
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (Root != null)
            {
                var position = transform.position;
                var labelStyle = new GUIStyle
                {
                    fontSize = 7,
                    normal = { textColor = Color.green },
                    alignment = TextAnchor.MiddleCenter
                };
                
                UnityEditor.Handles.color = new Color(0, 1, 0, 0.2f);
                UnityEditor.Handles.DrawSolidDisc(position, Vector3.up, InteractRadius);
                UnityEditor.Handles.Label(position + Vector3.up * 0.2f, $"{InteractiveObjectId}", labelStyle);
            }
        }
#endif
    }
}
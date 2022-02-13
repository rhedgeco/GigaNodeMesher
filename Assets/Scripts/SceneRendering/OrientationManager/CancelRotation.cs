using UnityEngine;

namespace GigaNodeMesher.SceneRendering.OrientationManager
{
    [ExecuteInEditMode]
    public class CancelRotation : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
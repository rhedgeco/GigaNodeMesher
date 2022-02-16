using UnityEngine;

namespace GigaNodeMesher.SceneRendering
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
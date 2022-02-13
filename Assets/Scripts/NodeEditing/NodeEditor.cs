using UnityEngine;

namespace GigaNodeMesher.NodeEditing
{
    public class NodeEditor : MonoBehaviour
    {
        [SerializeField] private Camera _viewingCamera;

        private void LateUpdate()
        {
            // Vector3 mousePos = Input.mousePosition;
            // Ray ray = _viewingCamera.ViewportPointToRay(_viewingCamera.ScreenToViewportPoint(mousePos));
            // _testObject.transform.position = ray.origin + ray.direction * _testDistance;
        }
    }
}
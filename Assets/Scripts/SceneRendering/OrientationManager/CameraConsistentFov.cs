using UnityEngine;

namespace GigaNodeMesher.SceneRendering.OrientationManager
{
    [RequireComponent(typeof(Camera))]
    [ExecuteInEditMode]
    public class CameraConsistentFov : MonoBehaviour
    {
        private Camera _camera;

        [SerializeField] private float fieldOfView = 60;
        [SerializeField] private Vector2 targetAspect = Vector2.one;

        private void OnEnable()
        {
            _camera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            float pixelWidth = _camera.pixelWidth;
            float pixelHeight = _camera.pixelHeight;
            float cameraAspectRatio = pixelWidth / pixelHeight;
            float targetAspectRatio = targetAspect.x / targetAspect.y;

            _camera.fieldOfView = cameraAspectRatio < targetAspectRatio
                ? Camera.VerticalToHorizontalFieldOfView(fieldOfView, targetAspectRatio / cameraAspectRatio)
                : fieldOfView;
            _camera.aspect = cameraAspectRatio;
        }
    }
}
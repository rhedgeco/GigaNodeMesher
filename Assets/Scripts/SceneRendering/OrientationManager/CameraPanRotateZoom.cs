using System;
using UnityEngine;

namespace GigaNodeMesher.SceneRendering.OrientationManager
{
    [ExecuteInEditMode]
    public class CameraPanRotateZoom : MonoBehaviour
    {
        private Vector3 _currentPan;
        private float _currentPanRadius;
        private Vector2 _currentRot;
        private float _currentZoom;

        private Vector3 _panLerp;
        private Vector2 _rotLerp;
        private float _zoomLerp;

        [SerializeField] private float lerpSpeed = 1f;

        [Header("Pan"), SerializeField] private Vector3 panHome;
        [SerializeField] private float panHomeRadius = 1;
        [SerializeField] private float maxZoomPanRadius = 0;
        [SerializeField] private float homePanSpeed = 0.5f;
        [SerializeField] private float minZoomPanSpeed = 0.1f;

        [Header("Rotate"), SerializeField] private Vector2 rotateHome;
        [SerializeField] private float rotationSpeed = 25f;

        [Header("Zoom"), SerializeField] private float zoomHome = 1;
        [SerializeField] private float zoomSpeed = 10f;
        [SerializeField] private float minZoomLevel = 0.1f;
        [SerializeField] private float maxZoomLevel = 2f;

        private void Awake()
        {
            HardResetHome();
        }

        private void Update()
        {
            if (Application.isPlaying) UpdateLerp();
            else HardResetHome();
        }
        
        public void ResetHome()
        {
            SetPanLerp(panHome);
            SetRotLerp(rotateHome);
            SetZoomLerp(zoomHome);
        }

        private void HardResetHome()
        {
            SetPan(panHome);
            SetRotate(rotateHome);
            SetZoom(zoomHome);
            SetPanLerp(panHome);
            SetRotLerp(rotateHome);
            SetZoomLerp(zoomHome);
        }

        public void PanDelta(Vector2 delta)
        {
            Transform t = transform;
            Vector3 delta3d = Vector3.zero;
            delta3d += t.right * delta.x;
            delta3d += t.up * delta.y;

            float panSpeed = homePanSpeed;
            if (_currentZoom < zoomHome)
            {
                float zoomLerp = (_currentZoom - minZoomLevel) / (zoomHome - minZoomLevel);
                panSpeed = Mathf.Lerp(minZoomPanSpeed, homePanSpeed, zoomLerp);
            }

            Vector3 newPan = _currentPan + delta3d * panSpeed;
            SetPan(newPan);
            _panLerp = newPan; // stop all lerping
        }

        public void RotateDelta(Vector2 delta)
        {
            Vector2 newRot = _currentRot + delta * rotationSpeed;
            SetRotate(newRot);
            _rotLerp = newRot; // stop all lerping
        }

        public void ZoomDelta(float delta)
        {
            float newZoom = _currentZoom - delta * zoomSpeed;
            SetZoom(newZoom);
            _zoomLerp = newZoom; // stop all lerping
        }

        public void SetRightView() => SetRotLerp(new Vector2(-90, 0));
        public void SetLeftView() => SetRotLerp(new Vector2(90, 0));
        public void SetFrontView() => SetRotLerp(new Vector2(0, 0));
        public void SetBackView() => SetRotLerp(new Vector2(180, 0));
        public void SetTopView() => SetRotLerp(new Vector2(0, -90));
        public void SetBottomView() => SetRotLerp(new Vector2(0, 90));

        private void SetPanLerp(Vector3 target)
        {
            _panLerp = ValidatePan(target);
        }

        private void SetRotLerp(Vector2 target)
        {
            _rotLerp = ValidateRot(target);
            
            // convert current and lerp to positive values
            if (_rotLerp.x < 0) _rotLerp.x += 360;
            if (_currentRot.x < 0) _currentRot.x += 360;
            
            // reset values spin would be more than 180
            if (_currentRot.x - _rotLerp.x > 180 ) _currentRot.x -= 360;
            else if (_rotLerp.x - _currentRot.x > 180 ) _rotLerp.x -= 360;
        }

        private void SetZoomLerp(float target)
        {
            _zoomLerp = ValidateZoom(target);
        }

        private void UpdateLerp()
        {
            const float floatDiff = 0.0001f;
            if (Vector3.Distance(_currentPan, _panLerp) > floatDiff)
                SetPan(Vector3.Lerp(_currentPan, _panLerp, Time.deltaTime * lerpSpeed));
            if (Vector2.Distance(_currentRot, _rotLerp) > floatDiff)
                SetRotate(Vector2.Lerp(_currentRot, _rotLerp, Time.deltaTime * lerpSpeed));
            if (Math.Abs(_currentZoom - _zoomLerp) > floatDiff)
                SetZoom(Mathf.Lerp(_currentZoom, _zoomLerp, Time.deltaTime * lerpSpeed));
        }

        private void SetPan(Vector3 newPan)
        {
            newPan = ValidatePan(newPan);
            transform.position = newPan;
            _currentPan = newPan;
        }

        private void SetRotate(Vector2 newRotation)
        {
            newRotation = ValidateRot(newRotation);
            transform.rotation = Quaternion.Euler(-newRotation.y, newRotation.x, 0);
            _currentRot = newRotation;
        }

        private void SetZoom(float newZoom)
        {
            newZoom = ValidateZoom(newZoom);
            transform.localScale = new Vector3(1, 1, newZoom);
            _currentZoom = newZoom;

            SetPan(_currentPan); // revalidate pan after zooming
        }

        private Vector3 ValidatePan(Vector3 newPan)
        {
            _currentPanRadius = panHomeRadius;
            if (_currentZoom > zoomHome)
            {
                float zoomLerp = (_currentZoom - zoomHome) / (maxZoomLevel - zoomHome);
                _currentPanRadius = Mathf.Lerp(panHomeRadius, maxZoomPanRadius, zoomLerp);
            }

            Vector3 panCheck = newPan - panHome;
            if (panCheck.magnitude > _currentPanRadius)
                panCheck = panCheck.normalized * _currentPanRadius;
            panCheck += panHome;
            return panCheck;
        }

        private Vector2 ValidateRot(Vector2 newRotation)
        {
            newRotation.x %= 360;
            newRotation.y = Mathf.Clamp(newRotation.y, -90, 90);
            return newRotation;
        }

        private float ValidateZoom(float newZoom)
        {
            return Mathf.Clamp(newZoom, minZoomLevel, maxZoomLevel);
        }
    }
}
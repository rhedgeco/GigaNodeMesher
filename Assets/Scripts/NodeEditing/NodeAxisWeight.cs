using GigaNodeMesher.Extensions;
using UnityEngine;

namespace GigaNodeMesher.NodeEditing
{
    [RequireComponent(typeof(SphereCollider))]
    public class NodeAxisWeight : MonoBehaviour
    {
        [SerializeField] private WeightDirection weightDirection;
        [SerializeField, Range(0, 1)] private float weight = 0.5f;
        [SerializeField] private Mesh nodeMesh; 
        [SerializeField, Range(0,0.1f)] private float nodeRadius = 0.04f;
        [SerializeField] private Color normalColor = NodeTools.Purple;
        [SerializeField] private Color highlightColor = NodeTools.Purple;
        [SerializeField] private Color pressedColor = NodeTools.Purple;

        public WeightDirection Direction => weightDirection;

        private Vector2 _dragStart;
        private float _dragStartWeight;
        private bool _dragging = false;
        private bool _hovering = false;

        public float Weight
        {
            get => weight;
            set => weight = Mathf.Clamp(value, 0, 1);
        }

        private SphereCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            GlobalNodeEditorData.AddWeightModeListener(OnWeightModeChanged);
        }

        private void Update()
        {
            Vector3 dir = GetDirectionVector();
            _collider.radius = nodeRadius;
            _collider.center = dir * Weight;
            
            if (!_dragging) return;
            Camera cam = Camera.main;
            if (!cam) return;
            
            Vector3 origin = transform.position;
            Vector2 sliderLine = cam.WorldToScreenPoint(origin + dir) - cam.WorldToScreenPoint(origin);
            Vector2 mouseLine = Input.mousePosition.ToVector2() - _dragStart;
            mouseLine /= sliderLine.magnitude;
            sliderLine /= sliderLine.magnitude;
            float slideAmount = Vector2.Dot(mouseLine, sliderLine);
            Weight = _dragStartWeight + slideAmount;
        }

        private void OnWeightModeChanged(bool show)
        {
            _collider.enabled = show;
        }

        private void OnMouseEnter() => _hovering = true;
        private void OnMouseExit() => _hovering = false;
        private void OnMouseUp() => _dragging = false;

        private void OnMouseDown()
        {
            _dragging = true;
            _dragStart = Input.mousePosition;
            _dragStartWeight = Weight;
        }

        private void OnRenderObject()
        {
            if (!_collider.enabled) return;
            if (!NodeTools.ShouldDraw(gameObject.layer)) return;
            NodeTools.StartColor(_dragging ? pressedColor : _hovering ? highlightColor : normalColor);
            
            Matrix4x4 matrix = Matrix4x4.TRS(GetDirectionVector() * Weight,
                Quaternion.identity, Vector3.one * nodeRadius * 2);
            Graphics.DrawMeshNow(nodeMesh, matrix);
        }

        public Vector3 GetDirectionVector()
        {
            switch (weightDirection)
            {
                case WeightDirection.PositiveX:
                    return Vector3.right;
                case WeightDirection.NegativeX:
                    return Vector3.left;
                case WeightDirection.PositiveY:
                    return Vector3.up;
                case WeightDirection.NegativeY:
                    return Vector3.down;
                case WeightDirection.PositiveZ:
                    return Vector3.forward;
                default: // WeightDirection.NegativeZ
                    return Vector3.back;
            }
        }

        public enum WeightDirection
        {
            PositiveX,
            NegativeX,
            PositiveY,
            NegativeY,
            PositiveZ,
            NegativeZ,
        }
    }
}
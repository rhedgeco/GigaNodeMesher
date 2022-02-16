using UnityEngine;

namespace GigaNodeMesher.NodeEditing
{
    [RequireComponent(typeof(MeshRenderer))]
    public class WeightNode : MonoBehaviour
    {
        private static readonly int RimFill = Shader.PropertyToID("_RimFill");
        private static readonly int RimColor = Shader.PropertyToID("_RimColor");

        [SerializeField, Range(0, 7)] private int weightIndex;
        [SerializeField] private float dragDistance = 100f;
        [SerializeField] private float hoverColorAdd = 0.1f;

        private Material _material;
        private bool _dragging;
        private bool _inside;
        private Vector2 _mouseDown;
        private float _downValue;
        private Color _color;
        private Color _rimColor;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            _color = _material.color;
            _rimColor = _material.GetColor(RimColor);
        }

        private void Update()
        {
            if (_dragging)
            {
                float delta = (Input.mousePosition.y - _mouseDown.y) / dragDistance;
                MyHandler().WeightRatio = _downValue + delta;
            }
            
            if (_inside) StartHighlight();
            else if (!_dragging) EndHighlight();

            _material.SetFloat(RimFill, MyHandler().WeightRatio);
        }

        private void OnMouseEnter() => _inside = true;
        private void OnMouseExit() => _inside = false;

        private void OnMouseDown()
        {
            _mouseDown = Input.mousePosition;
            _downValue = MyHandler().WeightRatio;
            _dragging = true;
        }

        private void OnMouseUp()
        {
            _dragging = false;
        }

        private void StartHighlight()
        {
            _material.color = _color + new Color(hoverColorAdd, hoverColorAdd, hoverColorAdd);
            _material.SetColor(RimColor, _rimColor + new Color(hoverColorAdd, hoverColorAdd, hoverColorAdd));
        }
        
        private void EndHighlight()
        {
            _material.color = _color;
            _material.SetColor(RimColor, _rimColor);
        }

        private WeightHandler MyHandler()
        {
            return GlobalNodeEditorData.GetWeightHandler(weightIndex);
        }
    }
}
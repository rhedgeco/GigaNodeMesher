using UnityEngine;
using UnityEngine.Events;

namespace GigaNodeMesher.UiManagement
{
    [RequireComponent(typeof(MeshRenderer))]
    public class ButtonObject : MonoBehaviour
    {
        [SerializeField] private UnityEvent onClick;
        [SerializeField] private Color hoverColor = Color.white;
        [SerializeField] private Color pressColor = Color.white;

        private bool _over;
        private Material _material;
        private Color _defaultColor;

        private void Awake()
        {
            EnsureMaterial();
        }

        private void OnMouseEnter()
        {
            _over = true;
            if (!EnsureMaterial()) return;
            _material.color = hoverColor;
        }

        private void OnMouseExit()
        {
            _over = false;
            if (!EnsureMaterial()) return;
            _material.color = _defaultColor;
        }

        private void OnMouseDown()
        {
            if (!_over) return;
            if (!EnsureMaterial()) return;
            _material.color = pressColor;
        }

        private void OnMouseUpAsButton()
        {
            onClick.Invoke();
            if (!EnsureMaterial()) return;
            _material.color = hoverColor;
        }

        private bool EnsureMaterial()
        {
            if (_material != null) return true;
            _material = GetComponent<MeshRenderer>().material;
            if (_material == null)
            {
                Debug.LogWarning("No material assigned to MeshRenderer, cannot change color");
                return false;
            }

            _defaultColor = _material.color;
            return true;
        }
    }
}
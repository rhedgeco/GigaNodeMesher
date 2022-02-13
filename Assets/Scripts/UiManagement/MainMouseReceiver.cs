using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GigaNodeMesher.UiManagement
{
    [RequireComponent(typeof(RectTransform))]
    public class MainMouseReceiver : MonoBehaviour, IDragHandler, IScrollHandler
    {
        private bool _modifier;

        [SerializeField] private KeyCode modifierKey;
        [SerializeField] private KeyCode modifierKey2;
        [SerializeField] private UnityEvent<Vector2> onRotate;
        [SerializeField] private UnityEvent<Vector2> onPan;
        [SerializeField] private UnityEvent<float> onZoom;
        [SerializeField] private UnityEvent<bool> onModify;

        private void Update()
        {
            if ((Input.GetKey(modifierKey) || Input.GetKey(modifierKey2)) && !_modifier)
            {
                _modifier = true;
                onModify.Invoke(true);
            }
            if (!Input.GetKey(modifierKey) && !Input.GetKey(modifierKey2) && _modifier)
            {
                _modifier = false;
                onModify.Invoke(false);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right) return;
            if (Input.GetKey(modifierKey)) onPan.Invoke(-eventData.delta);
            else onRotate.Invoke(eventData.delta);
        }

        public void OnScroll(PointerEventData eventData)
        {
            if (!eventData.IsScrolling()) return;
            onZoom.Invoke(eventData.scrollDelta.y);
        }
    }
}
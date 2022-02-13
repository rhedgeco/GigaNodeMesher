using GigaNodeMesher.NodeEditing;
using UnityEngine;
using UnityEngine.UI;

namespace GigaNodeMesher.UiManagement
{
    [RequireComponent(typeof(Text))]
    public class BoundaryModeLabel : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            GlobalNodeEditorData.AddBoundaryModeListener(UpdateLabel);
        }

        public void UpdateLabel(int mode)
        {
            _text.text = $"Case {mode}";
        }
    }
}
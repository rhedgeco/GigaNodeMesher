using GigaNodeMesher.NodeEditing;
using GigaNodeMesher.NodeEditing.BoundaryEditors;
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

        public void UpdateLabel(BoundaryMode mode)
        {
            _text.text = $"Case {(int) mode}";
        }
    }
}
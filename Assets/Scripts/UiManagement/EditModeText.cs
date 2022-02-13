using GigaNodeMesher.NodeEditing;
using UnityEngine;
using UnityEngine.UI;

namespace GigaNodeMesher.UiManagement
{
    [RequireComponent(typeof(Text))]
    public class EditModeText : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            GlobalNodeEditorData.AddEditModeListener(UpdateText);
        }

        public void UpdateText(EditMode mode)
        {
            _text.text = mode.ToString();
        }
    }
}
using GigaNodeMesher.NodeEditing;
using UnityEngine;
using UnityEngine.UI;

namespace GigaNodeMesher.UiManagement
{
    [RequireComponent(typeof(Text))]
    public class WeightText : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            GlobalNodeEditorData.AddWeightListener(UpdateWeightText);
        }

        public void UpdateWeightText(int weight)
        {
            _text.text = $"Node Weight: {weight:00}/{GlobalNodeEditorData.WeightMax}";
        }
    }
}
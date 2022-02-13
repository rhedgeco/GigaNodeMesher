using UnityEngine;
using UnityEngine.UI;

namespace GigaNodeMesher.UiManagement
{
    [RequireComponent(typeof(Text))]
    public class RotatePanText : MonoBehaviour
    {
        private Text _text;
        
        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public void Modify(bool modify)
        {
            _text.text = modify ? "Pan" : "Rotate";
        }
    }
}
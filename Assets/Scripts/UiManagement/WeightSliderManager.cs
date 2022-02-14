using GigaNodeMesher.NodeEditing;
using UnityEngine;
using UnityEngine.UI;

namespace GigaNodeMesher.UiManagement
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Slider))]
    public class WeightSliderManager : MonoBehaviour
    {
        private Slider _slider;

        private void Awake()
        {
            SetHome();
        }

        private void Update()
        {
            if (Application.isPlaying) return;
            EnsureLoaded();
            _slider.minValue = 1;
            _slider.maxValue = GlobalNodeEditorData.WeightMax - 1;
            SetHome();
        }

        private void SetHome()
        {
            EnsureLoaded();
            _slider.value = GlobalNodeEditorData.WeightMax / 2;
        }

        private void EnsureLoaded()
        {
            if (_slider == null) _slider = GetComponent<Slider>();
        }
    }
}
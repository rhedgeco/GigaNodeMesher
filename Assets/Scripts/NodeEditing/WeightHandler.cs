using UnityEngine;
using UnityEngine.Events;

namespace GigaNodeMesher.NodeEditing
{
    public class WeightHandler
    {
        public const int WeightMax = 16;
        
        private int _weight = Mathf.RoundToInt(WeightMax / 2f);
        private UnityEvent<int> _onWeightChanged = new UnityEvent<int>();

        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value > WeightMax - 1 ? WeightMax - 1 : value < 1 ? 1 : value;
            }
        }

        public float WeightRatio => _weight / (float) WeightMax;

        public void AddWeightChangedListener(UnityAction<int> action) => _onWeightChanged.AddListener(action);
    }
}
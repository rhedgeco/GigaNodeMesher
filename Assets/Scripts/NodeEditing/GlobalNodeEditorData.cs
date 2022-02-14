using System;
using UnityEngine;
using UnityEngine.Events;

namespace GigaNodeMesher.NodeEditing
{
    public class GlobalNodeEditorData : MonoBehaviour
    {
        public const int WeightMax = 16;
        
        private static readonly UnityEvent<int> OnWeightChanged = new UnityEvent<int>();
        private static readonly UnityEvent<EditMode> OnEditModeChanged = new UnityEvent<EditMode>();
        private static readonly UnityEvent<int> OnBoundaryModeChanged = new UnityEvent<int>();

        private static int _weight = 8;
        private static EditMode _editMode = EditMode.Boundaries;
        private static int _boundaryMode = 1;

        public static int Weight => _weight;
        public static EditMode EditMode => _editMode;
        public static int BoundaryMode => _boundaryMode;

        private void Start()
        {
            OnWeightChanged.Invoke(Weight);
            OnEditModeChanged.Invoke(EditMode);
            OnBoundaryModeChanged.Invoke(BoundaryMode);
        }

        public static void AddWeightListener(UnityAction<int> act) => OnWeightChanged.AddListener(act);
        public static void AddEditModeListener(UnityAction<EditMode> act) => OnEditModeChanged.AddListener(act);
        public static void AddBoundaryModeListener(UnityAction<int> act) => OnBoundaryModeChanged.AddListener(act);

        public void SetWeight(Single value)
        {
            int v = (int) value;
            _weight = v > WeightMax ? WeightMax : v < 1 ? 1 : v;
            OnWeightChanged.Invoke(Weight);
        }

        public void ToggleEditMode()
        {
            _editMode = _editMode == EditMode.Boundaries ? EditMode.Volumes : EditMode.Boundaries;
            OnEditModeChanged.Invoke(EditMode);
        }

        public void IncrementBoundaryMode()
        {
            if (_boundaryMode == 3) _boundaryMode = 1;
            else _boundaryMode++;
            OnBoundaryModeChanged.Invoke(BoundaryMode);
        }

        public void DecrementBoundaryMode()
        {
            if (_boundaryMode == 1) _boundaryMode = 3;
            else _boundaryMode--;
            OnBoundaryModeChanged.Invoke(BoundaryMode);
        }
    }
}
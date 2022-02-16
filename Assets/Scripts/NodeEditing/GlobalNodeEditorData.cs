using System;
using GigaNodeMesher.NodeEditing.BoundaryEditors;
using GigaNodeMesher.NodeEditing.VolumeEditors;
using UnityEngine;
using UnityEngine.Events;

namespace GigaNodeMesher.NodeEditing
{
    public class GlobalNodeEditorData : MonoBehaviour
    {
        public const int WeightMax = 16;

        private static readonly UnityEvent<int> OnWeightChanged = new UnityEvent<int>();
        private static readonly UnityEvent<EditMode> OnEditModeChanged = new UnityEvent<EditMode>();
        private static readonly UnityEvent<BoundaryMode> OnBoundaryModeChanged = new UnityEvent<BoundaryMode>();
        private static readonly UnityEvent<VolumeMode> OnVolumeModeChanged = new UnityEvent<VolumeMode>();

        private static int _weight = 8;
        private static EditMode _editMode = EditMode.Boundaries;
        private static BoundaryMode _boundaryMode = BoundaryMode.One;
        private static VolumeMode _volumeMode = VolumeMode.One;

        public static int Weight => _weight;
        public static EditMode EditMode => _editMode;
        public static BoundaryMode BoundaryMode => _boundaryMode;
        public static VolumeMode VolumeMode => _volumeMode;

        private void Start()
        {
            OnWeightChanged.Invoke(Weight);
            OnEditModeChanged.Invoke(EditMode);
            OnBoundaryModeChanged.Invoke(BoundaryMode);
            OnVolumeModeChanged.Invoke(VolumeMode);
        }

        public static void AddWeightListener(UnityAction<int> act) => OnWeightChanged.AddListener(act);
        public static void AddEditModeListener(UnityAction<EditMode> act) => OnEditModeChanged.AddListener(act);

        public static void AddBoundaryModeListener(UnityAction<BoundaryMode> act) =>
            OnBoundaryModeChanged.AddListener(act);

        public static void AddVolumeModeListener(UnityAction<VolumeMode> act) =>
            OnVolumeModeChanged.AddListener(act);

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
            _boundaryMode = _boundaryMode switch
            {
                BoundaryMode.One => BoundaryMode.Two,
                BoundaryMode.Two => BoundaryMode.Three,
                BoundaryMode.Three => BoundaryMode.One,
                _ => _boundaryMode
            };
            OnBoundaryModeChanged.Invoke(BoundaryMode);
        }

        public void DecrementBoundaryMode()
        {
            _boundaryMode = _boundaryMode switch
            {
                BoundaryMode.One => BoundaryMode.Three,
                BoundaryMode.Two => BoundaryMode.One,
                BoundaryMode.Three => BoundaryMode.Two,
                _ => _boundaryMode
            };
            OnBoundaryModeChanged.Invoke(BoundaryMode);
        }
    }
}
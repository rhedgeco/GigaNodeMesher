using System;
using GigaNodeMesher.NodeEditing.BoundaryEditors;
using GigaNodeMesher.NodeEditing.VolumeEditors;
using UnityEngine;
using UnityEngine.Events;

namespace GigaNodeMesher.NodeEditing
{
    public class GlobalNodeEditorData : MonoBehaviour
    {
        private static readonly UnityEvent<EditMode> OnEditModeChanged = new UnityEvent<EditMode>();
        private static readonly UnityEvent<BoundaryMode> OnBoundaryModeChanged = new UnityEvent<BoundaryMode>();
        private static readonly UnityEvent<VolumeMode> OnVolumeModeChanged = new UnityEvent<VolumeMode>();
        
        private static EditMode _editMode = EditMode.Boundaries;
        private static BoundaryMode _boundaryMode = BoundaryMode.One;
        private static VolumeMode _volumeMode = VolumeMode.One;
        private static WeightHandler[] _weightHandlers;
        
        public static EditMode EditMode => _editMode;
        public static BoundaryMode BoundaryMode => _boundaryMode;
        public static VolumeMode VolumeMode => _volumeMode;

        private void Start()
        {
            OnEditModeChanged.Invoke(EditMode);
            OnBoundaryModeChanged.Invoke(BoundaryMode);
            OnVolumeModeChanged.Invoke(VolumeMode);
        }
        
        public static void AddEditModeListener(UnityAction<EditMode> act) => OnEditModeChanged.AddListener(act);

        public static void AddBoundaryModeListener(UnityAction<BoundaryMode> act) =>
            OnBoundaryModeChanged.AddListener(act);

        public static void AddVolumeModeListener(UnityAction<VolumeMode> act) =>
            OnVolumeModeChanged.AddListener(act);

        public static WeightHandler GetWeightHandler(int index)
        {
            if (_weightHandlers == null) CreateWeightHandlers();
            index = index > 8 ? 8 : index < 0 ? 0 : index;
            // ReSharper disable once PossibleNullReferenceException
            return _weightHandlers[index];
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

        private static void CreateWeightHandlers()
        {
            _weightHandlers = new WeightHandler[8];
            for (int i = 0; i < 8; i++) _weightHandlers[i] = new WeightHandler();
        }
    }
}
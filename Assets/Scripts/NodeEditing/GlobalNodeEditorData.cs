using GigaNodeMesher.NodeEditing.BoundaryEditors;
using GigaNodeMesher.NodeEditing.VolumeEditors;
using UnityEngine;
using UnityEngine.Events;

namespace GigaNodeMesher.NodeEditing
{
    public class GlobalNodeEditorData : MonoBehaviour
    {
        private static readonly UnityEvent<bool> OnWeightModeChanged = new UnityEvent<bool>();
        private static readonly UnityEvent<EditMode> OnEditModeChanged = new UnityEvent<EditMode>();
        private static readonly UnityEvent<BoundaryMode> OnBoundaryModeChanged = new UnityEvent<BoundaryMode>();
        private static readonly UnityEvent<VolumeMode> OnVolumeModeChanged = new UnityEvent<VolumeMode>();

        public static bool ShowWeightEditors { get; private set; } = false;
        public static EditMode EditMode { get; private set; } = EditMode.Boundaries;
        public static BoundaryMode BoundaryMode { get; private set; } = BoundaryMode.One;
        public static VolumeMode VolumeMode { get; private set; } = VolumeMode.One;

        private void Start()
        {
            OnWeightModeChanged.Invoke(ShowWeightEditors);
            OnEditModeChanged.Invoke(EditMode);
            OnBoundaryModeChanged.Invoke(BoundaryMode);
            OnVolumeModeChanged.Invoke(VolumeMode);
        }
        
        public static void AddWeightModeListener(UnityAction<bool> act) => OnWeightModeChanged.AddListener(act);
        public static void AddEditModeListener(UnityAction<EditMode> act) => OnEditModeChanged.AddListener(act);

        public static void AddBoundaryModeListener(UnityAction<BoundaryMode> act) =>
            OnBoundaryModeChanged.AddListener(act);

        public static void AddVolumeModeListener(UnityAction<VolumeMode> act) =>
            OnVolumeModeChanged.AddListener(act);

        public void SetShowWeights(bool show)
        {
            ShowWeightEditors = show;
            OnWeightModeChanged.Invoke(ShowWeightEditors);
        }

        public void ToggleEditMode()
        {
            EditMode = EditMode == EditMode.Boundaries ? EditMode.Volumes : EditMode.Boundaries;
            OnEditModeChanged.Invoke(EditMode);
        }

        public void IncrementBoundaryMode()
        {
            BoundaryMode = BoundaryMode switch
            {
                BoundaryMode.One => BoundaryMode.Two,
                BoundaryMode.Two => BoundaryMode.Three,
                BoundaryMode.Three => BoundaryMode.One,
                _ => BoundaryMode
            };
            OnBoundaryModeChanged.Invoke(BoundaryMode);
        }

        public void DecrementBoundaryMode()
        {
            BoundaryMode = BoundaryMode switch
            {
                BoundaryMode.One => BoundaryMode.Three,
                BoundaryMode.Two => BoundaryMode.One,
                BoundaryMode.Three => BoundaryMode.Two,
                _ => BoundaryMode
            };
            OnBoundaryModeChanged.Invoke(BoundaryMode);
        }
    }
}
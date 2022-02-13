using System;
using GigaNodeMesher.NodeEditing;
using UnityEngine;

namespace GigaNodeMesher.UiManagement
{
    public class EditModeVisibility : MonoBehaviour
    {
        [SerializeField] private EditMode visibleMode;

        private void Awake()
        {
            GlobalNodeEditorData.AddEditModeListener(UpdateVisibility);
        }

        public void UpdateVisibility(EditMode mode)
        {
            gameObject.SetActive(visibleMode == mode);
        }
    }
}
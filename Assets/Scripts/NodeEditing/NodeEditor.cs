using UnityEngine;

namespace GigaNodeMesher.NodeEditing
{
    public class NodeEditor : MonoBehaviour
    {
        public void SetEnabled(bool editorEnabled)
        {
            gameObject.SetActive(editorEnabled);
        }
    }
}
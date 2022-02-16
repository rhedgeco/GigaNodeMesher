using UnityEngine;

namespace GigaNodeMesher.NodeEditing.BoundaryEditors
{
    public class BoundaryCase1Editor : MonoBehaviour
    {
        private void OnRenderObject()
        {
            if (!NodeTools.ShouldDraw(gameObject.layer)) return;
            NodeTools.StartColor(Color.white);
            
            
        }
    }
}
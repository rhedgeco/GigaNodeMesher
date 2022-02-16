using UnityEngine;

namespace GigaNodeMesher.NodeEditing.BoundaryEditors
{
    public class BoundaryCase1Editor : NodeEditor
    {
        private void OnRenderObject()
        {
            if (!NodeTools.ShouldDraw(gameObject.layer)) return;
            NodeTools.StartColor(Color.white);
            
            
        }
    }
}
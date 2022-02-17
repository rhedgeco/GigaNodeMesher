using UnityEngine;

namespace GigaNodeMesher.NodeEditing.BoundaryEditors
{
    public class BoundaryCase1Editor : MonoBehaviour
    {
        [SerializeField] private NodeAxisWeight positiveZ;
        [SerializeField] private NodeAxisWeight positiveX;
        
        private void OnRenderObject()
        {
            if (!NodeTools.ShouldDraw(gameObject.layer)) return;
            NodeTools.StartColor(Color.white);
            
            // render outlines of workspace
            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);
            GL.Begin(GL.LINES);

            GL.Color(NodeTools.Gray);
            float xWeight = positiveX.Weight;
            float zWeight = positiveZ.Weight;
            GL.Vertex3(xWeight, 0, 0);
            GL.Vertex3(xWeight, 0, zWeight);
            GL.Vertex3(xWeight, 0, zWeight);
            GL.Vertex3(0, 0, zWeight);
            
            GL.Color(NodeTools.TransparentGray);
            for (int i = 1; i < 16; i++)
            {
                float dist = i / 16f;
                GL.Vertex3(xWeight * dist, 0, 0);
                GL.Vertex3(xWeight * dist, 0, zWeight);
                GL.Vertex3(xWeight, 0, zWeight * dist);
                GL.Vertex3(0, 0, zWeight * dist);
            }

            GL.End();
            GL.PopMatrix();
        }
    }
}
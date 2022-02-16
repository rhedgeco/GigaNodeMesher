using GigaNodeMesher.NodeEditing.BoundaryEditors;
using UnityEngine;

namespace GigaNodeMesher.NodeEditing
{
    [ExecuteInEditMode]
    public class NodeEditorManager : MonoBehaviour
    {
        [Header("Boundary Editors"), SerializeField] private BoundaryCase1Editor case1;

        private void Awake()
        {
            GlobalNodeEditorData.AddEditModeListener(OnEditModeChanged);
            GlobalNodeEditorData.AddBoundaryModeListener(OnBoundaryModeChanged);
        }

        private void OnEditModeChanged(EditMode mode)
        {
            if (mode != EditMode.Boundaries) DisableBoundaryEditors();
            else OnBoundaryModeChanged(GlobalNodeEditorData.BoundaryMode);
        }

        private void OnBoundaryModeChanged(BoundaryMode mode)
        {
            if (GlobalNodeEditorData.EditMode == EditMode.Volumes) return;
            case1.SetEnabled(mode == BoundaryMode.One);
        }

        private void DisableBoundaryEditors()
        {
            case1.SetEnabled(false);
        }

        private void OnRenderObject()
        {
            if (!RenderTools.ShouldDraw(gameObject.layer)) return;
            RenderTools.StartColor(Color.white);

            // render outlines of workspace
            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);
            GL.Begin(GL.LINES);

            // create X axis line
            GL.Color(NodeColors.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(1, 0, 0);

            // create Y axis line
            GL.Color(NodeColors.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 1, 0);

            // create Z axis line
            GL.Color(NodeColors.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 1);

            // draw all other cube lines
            GL.Color(NodeColors.Gray);
            GL.Vertex3(1, 0, 0);
            GL.Vertex3(1, 1, 0);
            GL.Vertex3(1, 1, 0);
            GL.Vertex3(0, 1, 0);
            GL.Vertex3(1, 0, 0);
            GL.Vertex3(1, 0, 1);
            GL.Vertex3(1, 0, 1);
            GL.Vertex3(0, 0, 1);
            GL.Vertex3(1, 0, 1);
            GL.Vertex3(1, 1, 1);
            GL.Vertex3(0, 0, 1);
            GL.Vertex3(0, 1, 1);
            GL.Vertex3(1, 1, 0);
            GL.Vertex3(1, 1, 1);
            GL.Vertex3(1, 1, 1);
            GL.Vertex3(0, 1, 1);
            GL.Vertex3(0, 1, 1);
            GL.Vertex3(0, 1, 0);

            GL.End();
            GL.PopMatrix();
        }
    }
}
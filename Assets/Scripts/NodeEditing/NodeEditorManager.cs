using GigaNodeMesher.NodeEditing.BoundaryEditors;
using GigaNodeMesher.NodeEditing.VolumeEditors;
using UnityEngine;

namespace GigaNodeMesher.NodeEditing
{
    [ExecuteInEditMode]
    public class NodeEditorManager : MonoBehaviour
    {
        [Header("Boundary Editors"), SerializeField] private BoundaryCase1Editor case1;
        [SerializeField] private BoundaryCase2Editor case2;
        [SerializeField] private BoundaryCase3Editor case3;

        private void Awake()
        {
            GlobalNodeEditorData.AddEditModeListener(OnEditModeChanged);
            GlobalNodeEditorData.AddBoundaryModeListener(OnBoundaryModeChanged);
        }

        private void OnEditModeChanged(EditMode mode)
        {
            OnBoundaryModeChanged(GlobalNodeEditorData.BoundaryMode);
            OnVolumeModeChanged(GlobalNodeEditorData.VolumeMode);
        }

        private void OnBoundaryModeChanged(BoundaryMode mode)
        {
            bool editing = GlobalNodeEditorData.EditMode == EditMode.Boundaries;
            case1.gameObject.SetActive(mode == BoundaryMode.One && editing);
            case2.gameObject.SetActive(mode == BoundaryMode.Two && editing);
            case3.gameObject.SetActive(mode == BoundaryMode.Three && editing);
        }

        private void OnVolumeModeChanged(VolumeMode mode)
        {
            bool editing = GlobalNodeEditorData.EditMode == EditMode.Volumes;
        }

        private void OnRenderObject()
        {
            if (!NodeTools.ShouldDraw(gameObject.layer)) return;
            NodeTools.StartColor(Color.white);

            // render outlines of workspace
            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);
            GL.Begin(GL.LINES);

            // create X axis line
            GL.Color(NodeTools.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(1, 0, 0);

            // create Y axis line
            GL.Color(NodeTools.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 1, 0);

            // create Z axis line
            GL.Color(NodeTools.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 1);

            // draw all other cube lines
            GL.Color(NodeTools.Gray);
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
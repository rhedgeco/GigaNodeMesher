using UnityEngine;

namespace GigaNodeMesher.NodeEditing
{
    public class NodeEditor : MonoBehaviour
    {
        private static readonly Color Gray = new Color(0.15f, 0.15f, 0.15f);
        private static readonly Color GridGray = new Color(0.15f, 0.15f, 0.15f, 0.10f);
        private static Material _lineMaterial;
        
        [SerializeField] private Camera _viewingCamera;

        private void LateUpdate()
        {
            
        }

        private void OnRenderObject()
        {
            Camera currentCam = Camera.current;
            if(currentCam.cameraType == CameraType.Preview) return;
            int layerMask = 1 << gameObject.layer;
            if ((layerMask & currentCam.cullingMask) == 0) return;
            
            CreateLineMaterial();
            if (GlobalNodeEditorData.EditMode == EditMode.Boundaries) RenderBoundaryEditor();
            else RenderVolumeEditor();
        }

        #region BoundaryRendering

        private void RenderBoundaryEditor()
        {
            if (GlobalNodeEditorData.BoundaryMode == 1) RenderCase1();
            //else if (GlobalNodeEditorData.BoundaryMode == 1) RenderCase2();
            //else if (GlobalNodeEditorData.BoundaryMode == 2) RenderCase3();
        }

        private void RenderCase1()
        {
            float weight = GlobalNodeEditorData.Weight / (float) GlobalNodeEditorData.WeightMax;
            
            // draw initial borders and grids
            _lineMaterial.SetPass(0);
            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);
            GL.Begin(GL.LINES);
            
            GL.Color(Gray);
            GL.Vertex3(weight, 0, 0);
            GL.Vertex3(weight, 0, weight);
            GL.Vertex3(weight, 0, weight);
            GL.Vertex3(0, 0, weight);
            
            GL.Color(GridGray);
            for (int i = 1; i < 16; i++)
            {
                float weightLevel = weight * i / 16f;
                GL.Vertex3(weightLevel, 0, 0);
                GL.Vertex3(weightLevel, 0, weight);
                GL.Vertex3(weight, 0, weightLevel);
                GL.Vertex3(0, 0, weightLevel);
            }
            
            GL.End();
            GL.PopMatrix();
        }

        #endregion

        #region VolumeRendering

        private void RenderVolumeEditor()
        {
            
        }

        #endregion

        private static void CreateLineMaterial()
        {
            if (_lineMaterial) return;
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            _lineMaterial = new Material(shader);
            _lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            _lineMaterial.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
            _lineMaterial.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            _lineMaterial.SetInt("_Cull", (int) UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            _lineMaterial.SetInt("_ZWrite", 0);
        }
    }
}
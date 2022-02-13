using UnityEngine;

namespace GigaNodeMesher.SceneRendering
{
    [ExecuteInEditMode]
    public class OutlineRenderer : MonoBehaviour
    {
        private static readonly Color Gray = new Color(0.15f, 0.15f, 0.15f);
        private static readonly Color Red = new Color(1, 0, 0);
        private static readonly Color Green = new Color(0, 1, 0);
        private static readonly Color Blue = new Color(0.1f, 0.1f, 1);
        private static Material _lineMaterial;

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

        private void OnRenderObject()
        {
            Camera currentCam = Camera.current;
            if(currentCam.cameraType == CameraType.Preview) return;
            int layerMask = 1 << gameObject.layer;
            if ((layerMask & currentCam.cullingMask) == 0) return;
            
            CreateLineMaterial();
            _lineMaterial.SetPass(0);

            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);
            GL.Begin(GL.LINES);

            // create X axis line
            GL.Color(Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(1, 0, 0);

            // create Y axis line
            GL.Color(Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 1, 0);

            // create Z axis line
            GL.Color(Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 1);

            // draw all other cube lines
            GL.Color(Gray);
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
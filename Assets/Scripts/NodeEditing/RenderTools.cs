using UnityEngine;

namespace GigaNodeMesher.NodeEditing
{
    public static class RenderTools
    {
        private static Material _material;
        private static readonly int SrcBlend = Shader.PropertyToID("_SrcBlend");
        private static readonly int DstBlend = Shader.PropertyToID("_DstBlend");
        private static readonly int Cull = Shader.PropertyToID("_Cull");
        private static readonly int ZWrite = Shader.PropertyToID("_ZWrite");

        public static bool ShouldDraw(int layer)
        {
            Camera currentCam = Camera.current;
            if(currentCam.cameraType == CameraType.Preview) return false;
            int layerMask = 1 << layer;
            return (layerMask & currentCam.cullingMask) != 0;
        }

        public static void StartColor(Color color)
        {
            if (!_material) CreateLineMaterial();
            _material.color = color;
            _material.SetPass(0);
        }
        
        private static void CreateLineMaterial()
        {
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            _material = new Material(shader) {hideFlags = HideFlags.HideAndDontSave};
            _material.SetInt(SrcBlend, (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
            _material.SetInt(DstBlend, (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            _material.SetInt(Cull, (int) UnityEngine.Rendering.CullMode.Off);
            _material.SetInt(ZWrite, 1);
        }
    }
}
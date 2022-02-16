using UnityEngine;

namespace GigaNodeMesher.NodeEditing
{
    public static class NodeTools
    {
        public static readonly Color Red = new Color(1, 0, 0);
        public static readonly Color Green = new Color(0, 1, 0);
        public static readonly Color Blue = new Color(0.1f, 0.1f, 1);
        public static readonly Color Purple = new Color(0.79f, 0.47f, 0.82f);
        public static readonly Color Gray = new Color(0.15f, 0.15f, 0.15f);
        public static readonly Color TransparentGray = new Color(0.15f, 0.15f, 0.15f, 0.15f);
        
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
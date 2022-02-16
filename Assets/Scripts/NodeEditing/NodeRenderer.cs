using UnityEngine;

namespace GigaNodeMesher.NodeEditing
{
    [CreateAssetMenu(fileName = "NodeRenderer", menuName = "NodeRenderer")]
    public class NodeRenderer : ScriptableObject
    {
        [SerializeField] private Mesh _weightNodeMesh;
        [SerializeField] private float _weightNodeScale;
        [SerializeField] private Mesh _vertexMesh;
        [SerializeField] private float _vertexScale;

        public Mesh WeightNodeMesh => _weightNodeMesh;
        public float WeightNodeScale => _weightNodeScale;
        public Mesh VertexMesh => _vertexMesh;
        public float VertexScale => _vertexScale;

        public void RenderWeightNodeNow(Vector3 position, float scale = 1f)
        {
            Matrix4x4 matrix = CreateMatrix(position, _weightNodeScale * scale);
            Graphics.DrawMeshNow(_weightNodeMesh, matrix);
        }

        public void RenderVertexNow(Vector3 position, float scale = 1f)
        {
            Matrix4x4 matrix = CreateMatrix(position, _vertexScale * scale);
            Graphics.DrawMeshNow(_vertexMesh, matrix);
        }

        private Matrix4x4 CreateMatrix(Vector3 pos, float scale)
        {
            return Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one * scale);
        }
    }
}
using UnityEngine;

namespace GigaNodeMesher.NodeEditing
{
    public class WeightNode : MonoBehaviour
    {
        [SerializeField, Range(0, 7)] private int weightIndex;
    }
}
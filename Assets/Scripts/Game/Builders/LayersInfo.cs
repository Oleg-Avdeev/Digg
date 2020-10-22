using Digg.Game.Layers;
using Digg.Game.Teasures;
using UnityEngine;

namespace Digg.Game.Builders
{
    [CreateAssetMenu]
    public sealed class LayersInfo : ScriptableObject
    {
        public Sprite[] TopVariants = default;
        public Sprite[] MiddleVariants = default;
        public Sprite[] BottomVariants = default;
        public Gradient DepthGradient = default;
        
        [Range(0,1)] 
        public float TreasureProbability = default;
        public Sprite[] TreasureVariants = default;
        public Treasure TreasurePrefab = default;
    }
}
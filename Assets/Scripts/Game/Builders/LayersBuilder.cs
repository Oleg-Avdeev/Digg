using Digg.Game.Layers;
using UnityEngine;

namespace Digg.Game.Builders
{
    // Build a layer queue for a single cell
    // Can be changed to build more complex layer objects
    // Or combine multiple layer info to get a more interesting queue

    // Can be abstracted further to have different builders for different
    // game modes, e.g. building a specific queue for even or odd cells

    public sealed class LayersBuilder
    {
        private LayersInfo _info;
        private int _maxDepth;

        public LayersBuilder(LayersInfo info, int depth)
        {
            _maxDepth = depth;
            _info = info;
        }

        public LayersQueue BuildLayersQueue()
        {
            var queue = new LayersQueue();
            
            for (int i = 0; i < _maxDepth; i++)
            {
                queue.AddLayer(CreateLayer(_info, i));
            }

            return queue;
        }

        public Layer CreateLayer(LayersInfo info, int depth)
        {
            var layer = new DirtLayer();
            
            Sprite sprite = default;

            if (depth == 0)
            {
                sprite = info.TopVariants.GetRandomElement();
            }
            else if (depth == _maxDepth - 1)
            {
                sprite = info.BottomVariants.GetRandomElement();
            }
            else
            {
                sprite = info.MiddleVariants.GetRandomElement();
            }

            if (depth != 0)
            {
                if (Random.Range(0f, 1f) < info.TreasureProbability)
                {
                    layer.SetTreasure(info.TreasurePrefab, info.TreasureVariants.GetRandomElement());
                }
            }

            var color = info.DepthGradient.Evaluate((float)depth/_maxDepth);

            layer.Initialize(sprite, color, depth == _maxDepth - 1);

            return layer;
        }
    }
}
using Digg.Game.Layers;
using UnityEngine;

namespace Digg.Game.Builders
{
    public sealed class LayersBuilder
    {
        private LayersInfo _info;
        private int _depth;

        public LayersBuilder(LayersInfo info, int depth)
        {
            _depth = depth;
            _info = info;
        }

        public LayersQueue BuildLayersQueue()
        {
            var queue = new LayersQueue();
            
            for (int i = 0; i < _depth; i++)
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
                int index = Random.Range(0, info.TopVariants.Length);
                sprite = info.TopVariants[index];
            }
            else if (depth == _depth - 1)
            {
                int index = Random.Range(0, info.BottomVariants.Length);
                sprite = info.BottomVariants[index];
            }
            else
            {
                int index = Random.Range(0, info.MiddleVariants.Length);
                sprite = info.MiddleVariants[index];
            }

            if (depth != 0)
            {
                if (Random.Range(0f, 1f) < info.TreasureProbability)
                {
                    int index = Random.Range(0, info.TreasureVariants.Length);
                    layer.SetTreasure(info.TreasurePrefab, info.TreasureVariants[index]);
                }
            }

            var color = info.DepthGradient.Evaluate((float)depth/_depth);

            layer.Initialize(sprite, color);

            return layer;
        }
    }
}
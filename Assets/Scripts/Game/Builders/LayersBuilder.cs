using Digg.Game.Layers;
using Digg.Game.Pools;
using Digg.Game.Teasures;
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
        private TreasurePool _treasurePool;
        private LayersInfo _info;
        private int _maxDepth;

        public LayersBuilder(LayersInfo info, int depth)
        {
            _treasurePool = new TreasurePool(info.TreasurePrefab, 5);
            _maxDepth = depth;
            _info = info;
        }

        public LayersQueue BuildLayersQueue(int x, int y)
        {
            var queue = new LayersQueue();
            
            var digs = Data.DataManager.Instance.GetDigsAt(x, y);
            var treasure = Data.DataManager.Instance.GetTreasureAt(x, y);

            for (int i = 0; i < _maxDepth; i++)
            {
                if (i >= digs)
                {
                    queue.AddLayer(CreateLayer(_info, i, digs, treasure));
                }
            }

            return queue;
        }

        public Layer CreateLayer(LayersInfo info, int depth, int skip, bool forceTreasure)
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
                if (forceTreasure || (skip == 0 && Random.Range(0f, 1f) < info.TreasureProbability))
                {
                    var treasure = new Treasure();
                    treasure.Initialize(info.TreasureVariants.GetRandomElement(), _treasurePool.GetTreasure);
                    layer.SetTreasure(treasure);
                }
            }

            var color = info.DepthGradient.Evaluate((float)depth/_maxDepth);

            layer.Initialize(sprite, color, depth == _maxDepth - 1);

            return layer;
        }
    }
}
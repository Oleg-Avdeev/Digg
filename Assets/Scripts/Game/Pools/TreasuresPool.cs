using Digg.Game.Teasures;
using UnityEngine;

namespace Digg.Game.Pools
{
    public sealed class TreasurePool
    {
        private Pool<TreasureRenderer> _treasuresPool;
        private TreasureRenderer _treasurePrefab;

        public TreasurePool(TreasureRenderer prefab, int startingCount)
        {
            if (_treasuresPool == null)
            {
                _treasurePrefab = prefab;
                _treasuresPool = new Pool<TreasureRenderer>(CreateTreasure, startingCount);
            }
        }

        public TreasureRenderer GetTreasure()
        {
            var treasure = _treasuresPool.GetObject();
            return treasure;
        }

        private TreasureRenderer CreateTreasure()
        {
            var treasure = GameObject.Instantiate(_treasurePrefab, Vector3.zero, Quaternion.identity);
            treasure.Reset();
            return treasure;
        }
    }
}
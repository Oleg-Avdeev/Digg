using System.Collections.Generic;
using Digg.Game.Teasures;
using UnityEngine;

namespace Digg.Game.Layers
{
    public abstract class Layer
    {
        public Sprite DecalSprite { get; private set; }
        public Color DepthColor { get; private set; }
        public Treasure TreasurePrefab { get; private set; }
        public Sprite TreasureSprite { get; private set; }
        
        protected bool _hasTreasure = false;

        public void SetTreasure(Treasure prefab, Sprite sprite)
        {
            TreasurePrefab = prefab;
            TreasureSprite = sprite;
            _hasTreasure = true;
        }

        public virtual void Initialize(Sprite decal, Color color)
        {
            DepthColor = color;
            DecalSprite = decal;
        }

        public virtual void TreasureRemovedCallback()
        {
            _hasTreasure = false;
        }

        public abstract bool CanRemove();
        public abstract void Destroy();
    }
}
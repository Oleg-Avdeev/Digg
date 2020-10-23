using System.Collections.Generic;
using Digg.Game.Teasures;
using UnityEngine;

namespace Digg.Game.Layers
{
    // Can be overriden to have different behaviour, e.g.
    // Dirt Layer — 1 Tap to break
    // Hard Dirt  — N Taps to break
    // Rock       - Can't be broken with shovel, requires a pickaxe
    // Chest      — Requires extra tap to open and get treasure, plays animation
    // Etc... 

    public abstract class Layer
    {
        public Sprite DecalSprite { get; private set; }
        public Color DepthColor { get; private set; }
        public Treasure Treasure { get; private set; }

        protected bool _hasTreasure = false;
        protected bool _isLast = false;

        public void SetTreasure(Treasure treasure)
        {
            Treasure = treasure;
            _hasTreasure = true;
        }

        public virtual void Initialize(Sprite decal, Color color, bool isLast)
        {
            DepthColor = color;
            DecalSprite = decal;
            _isLast = isLast;
        }

        public virtual void TreasureRemovedCallback()
        {
            _hasTreasure = false;
        }

        public virtual bool CanRemove() => !_isLast;
        public abstract void Destroy();
    }
}
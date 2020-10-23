using UnityEngine;
using System;

namespace Digg.Game.Teasures
{
    public sealed class Treasure
    {
        public Sprite Sprite { get; private set; }
        
        private Func<TreasureRenderer> _createTreasure;

        public void Initialize(Sprite sprite, Func<TreasureRenderer> createTreasure)
        {
            Sprite = sprite;
            _createTreasure = createTreasure;
        }

        public TreasureRenderer Create()
        {
            var renderer = _createTreasure?.Invoke();
            renderer.SetTreasure(this);
            return renderer;
        }
    }
}
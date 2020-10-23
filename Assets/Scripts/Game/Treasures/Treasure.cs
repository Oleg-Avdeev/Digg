using UnityEngine;
using System;

namespace Digg.Game.Teasures
{
    public sealed class Treasure
    {
        public Sprite Sprite { get; private set; }
        
        private Func<TreasureRenderer> _createTreasure;
        private int _x;
        private int _y;

        public void Initialize(Sprite sprite, Func<TreasureRenderer> createTreasure)
        {
            Sprite = sprite;
            _createTreasure = createTreasure;
        }

        public TreasureRenderer Create(int x, int y)
        {
            _x = x; _y = y;
            Data.DataManager.Instance.AddUncoveredTreasure(x, y);
            var renderer = _createTreasure?.Invoke();
            renderer.OnCollected += RemoveTreasure;
            renderer.SetTreasure(this);

            return renderer;
        }

        private void RemoveTreasure()
        {
            Data.DataManager.Instance.RemoveUncoveredTreasure(_x, _y);
        }
    }
}
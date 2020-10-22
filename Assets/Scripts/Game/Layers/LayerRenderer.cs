using System.Collections.Generic;
using UnityEngine;

namespace Digg.Game.Layers
{
    public sealed class LayerRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backgroundRenderer = default;
        [SerializeField] private SpriteRenderer _spriteRenderer = default;

        public void SetLayer(Layer layer)
        {
            _backgroundRenderer.color = layer.DepthColor;
            _spriteRenderer.sprite = layer.DecalSprite;

            if (layer.TreasureSprite != null)
            {
                var treasure = Instantiate(layer.TreasurePrefab);
                treasure.transform.SetParent(transform, false);
                treasure.transform.localPosition = Vector3.zero;
                treasure.SetSprite(layer.TreasureSprite);
                treasure.SetOnCollectedCallback(layer.TreasureRemovedCallback);

                _spriteRenderer.sprite = null;
            }
        }
    }
}
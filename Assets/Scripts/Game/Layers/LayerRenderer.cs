using System.Collections.Generic;
using Digg.Game.Teasures;
using UnityEngine;

namespace Digg.Game.Layers
{
    public sealed class LayerRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backgroundRenderer = default;
        [SerializeField] private SpriteRenderer _spriteRenderer = default;
        
        private TreasureRenderer _currentTreasure;

        public void SetLayer(Layer layer, int x, int y)
        {
            _backgroundRenderer.color = layer.DepthColor;
            _spriteRenderer.sprite = layer.DecalSprite;

            if (layer.Treasure != null)
            {
                _currentTreasure = layer.Treasure.Create(x, y);
                
                _currentTreasure.OnCollected += layer.TreasureRemovedCallback;
                _currentTreasure.OnDropped += ResetTreasurePosition;
                _currentTreasure.OnDropped += HideDecal;
                _currentTreasure.OnPickUp += ShowDecal;
                _currentTreasure.Show();

                ResetTreasurePosition();
                HideDecal();
            }
        }

        private void ShowDecal() => _spriteRenderer.enabled = true;
        private void HideDecal() => _spriteRenderer.enabled = false;
        
        private void ResetTreasurePosition()
        {
            _currentTreasure.transform.SetParent(transform, false);
            _currentTreasure.transform.localPosition = Vector3.zero;
        }
    }
}
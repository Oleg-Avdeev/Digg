using Digg.Game.Layers;
using UnityEngine;

namespace Digg.Game
{
    public sealed class Cell : PoolBehaviour
    {
        [SerializeField] private LayerRenderer _layerRenderer = default;

        private LayersQueue _layersQueue;

        public void SetLayersQueue(LayersQueue layersQueue)
        {
            _layersQueue = layersQueue;
            RenderNewLayer();
        }

        public void RenderNewLayer()
        {
            _layerRenderer.SetLayer(_layersQueue.GetCurrentLayer());
        }

        private void OnMouseDown()
        {
            if (_layersQueue.GetCurrentLayer().CanRemove())
            {
                if (Player.Instance.TryUseShovel())
                {
                    _layersQueue.RemoveLayer();
                    RenderNewLayer();
                }
            }
        }

        public override void Reset()
        {
            gameObject.SetActive(false);
        }
    }
}
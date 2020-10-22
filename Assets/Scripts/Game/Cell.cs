using Digg.Game.Layers;
using UnityEngine;

namespace Digg.Game
{
    public sealed class Cell : MonoBehaviour
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

        public void Destroy()
        {
            Destroy(gameObject);
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
    }
}
using Digg.Game.Layers;
using UnityEngine;
using DG.Tweening;

namespace Digg.Game
{
    public sealed class Cell : PoolBehaviour
    {
        [SerializeField] private LayerRenderer _layerRenderer = default;
        [SerializeField] private Ease _appearEase = default;

        private LayersQueue _layersQueue;
        private int _x;
        private int _y;

        public void SetLayersQueue(LayersQueue layersQueue, int x, int y)
        {
            _x = x; _y = y;
            _layersQueue = layersQueue;
            RenderNewLayer();
        }

        public void RenderNewLayer()
        {
            _layerRenderer.SetLayer(_layersQueue.GetCurrentLayer(), _x, _y);
        }

        private void OnMouseDown()
        {
            if (_layersQueue.GetCurrentLayer().CanRemove())
            {
                if (Player.Instance.TryUseShovel())
                {
                    _layersQueue.RemoveLayer();
                    RenderNewLayer();

                    Data.DataManager.Instance.AddDig(_x, _y);
                }
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.5f).SetEase(_appearEase);
        }

        public override void Reset()
        {
            gameObject.SetActive(false);
            _layersQueue?.Destroy();
            _layersQueue = null;
        }
    }
}
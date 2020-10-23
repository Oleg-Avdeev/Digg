using Digg.Game.Layers;
using UnityEngine;
using System;

namespace Digg.Game.Teasures
{
    public sealed class TreasureRenderer : PoolBehaviour
    {
        public event Action OnPickUp;
        public event Action OnDropped;
        public event Action OnCollected;

        [SerializeField] private SpriteRenderer _renderer = default;
        [SerializeField] private Collider2D _collider = default;
        [SerializeField] private float _sizeCoefficient = 1.5f;

        private bool _dragging = false;
        private Camera _gameCamera;
        private Material _material;

        public void SetTreasure(Treasure treasure)
        {
            if (_gameCamera == null)
            {
                _gameCamera = Camera.main;
                _material = _renderer.material;
            }

            _renderer.sprite = treasure.Sprite;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void OnMouseDown()
        {
            _dragging = true;
            
            transform.localScale = Vector3.one * _sizeCoefficient;
            _material.SetFloat("_Highlight", 1f);
            _collider.enabled = false;
            
            OnPickUp?.Invoke();
            Chest.Instance.Open();
        }

        private void OnMouseUp()
        {
            _dragging = false;

            transform.localScale = Vector3.one;
            _material.SetFloat("_Highlight", 0f);
            _collider.enabled = true;
            
            Chest.Instance.Close();

            if (Chest.Instance.IsHovered())
            {
                Player.Instance.AddTreasure();
                OnCollected?.Invoke();
                Free();
            }
            else
            {
                OnDropped?.Invoke();
            }
        }

        private void Update()
        {
            if (_dragging)
            {
                var point = Input.mousePosition;
                point = _gameCamera.ScreenToWorldPoint(point);
                point.z = 0;
                
                transform.position = point;
            }
        }

        public override void Reset()
        {
            gameObject.SetActive(false);
            OnCollected = null;
            OnDropped = null;
            OnPickUp = null;
        }

        private void OnDestroy()
        {
            Destroy(_material);
        }
    }
}
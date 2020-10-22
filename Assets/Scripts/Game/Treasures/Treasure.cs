using Digg.Game.Layers;
using UnityEngine;
using System;

namespace Digg.Game.Teasures
{
    public sealed class Treasure : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer = default;
        [SerializeField] private Collider2D _collider = default;
        [SerializeField] private float _sizeCoefficient = 1.5f;

        private bool _dragging = false;
        private Camera _gameCamera;
        private Action _onCollected;
        private Material _material;

        public void SetSprite(Sprite sprite)
        {
            _renderer.sprite = sprite;
            _material = _renderer.material;
        }

        public void SetOnCollectedCallback(Action onCollected)
        {
            _onCollected = onCollected;
        }

        private void OnMouseDown()
        {
            if (_gameCamera == null)
            {
                _gameCamera = Camera.main;
            }

            _dragging = true;
            transform.localScale = Vector3.one * _sizeCoefficient;
            _material.SetFloat("_Highlight", 1f);
            _collider.enabled = false;
            Chest.Instance.Open();
        }

        private void OnMouseUp()
        {
            _dragging = false;
            transform.localScale = Vector3.one;
            _onCollected?.Invoke();
            _material.SetFloat("_Highlight", 0f);
            _collider.enabled = true;
            Chest.Instance.Close();

            if (Chest.Instance.IsHovered())
            {
                gameObject.SetActive(false);
                Player.Instance.AddTreasure();
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

        private void OnDestroy()
        {
            Destroy(_material);
        }
    }
}
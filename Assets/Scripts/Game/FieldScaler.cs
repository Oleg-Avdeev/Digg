using UnityEngine;

namespace Digg.Game
{
    public sealed class FieldScaler : MonoBehaviour
    {
        [SerializeField] private Transform _cellsContainer = default;
        [SerializeField] private Camera _gameCamera = default;

        [SerializeField] private float _cellSize = 1;
        [SerializeField] private float _widthPercent = 0.7f;
        [SerializeField] private float _heightPercent = 1f;
        [SerializeField] private float _padding = 0.5f;

        private int _width = 0;
        private int _height = 0;

        public void Scale(int width, int height)
        {
            float worldHeight = _gameCamera.orthographicSize * 2 * _heightPercent;
            float worldWidth = worldHeight / Screen.currentResolution.height * Screen.currentResolution.width * _widthPercent;

            float fieldWidth = width * _cellSize + 2 * _padding;
            float fieldHeight = height * _cellSize + 2 * _padding;

            float scale = Mathf.Min(worldWidth / fieldWidth, worldHeight / fieldHeight);

            _cellsContainer.localScale = Vector3.one * scale;
            _cellsContainer.localPosition = -(_cellSize * scale / 2f) * new Vector3(width - 1, height - 1, 0);

            _width = width;
            _height = height;
        }

        private void OnDrawGizmos()
        {
            float worldHeight = _gameCamera.orthographicSize * 2;
            float worldWidth = worldHeight / Screen.currentResolution.height * Screen.currentResolution.width;

            float w = worldWidth * _widthPercent;
            float h = worldHeight * _heightPercent;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(w, h, 0));
        }

        public void Rescale() => Scale(_width, _height);
    }
}
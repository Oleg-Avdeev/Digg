using System.Collections.Generic;
using Digg.Game.Builders;
using UnityEngine;
using System;

namespace Digg.Game
{
    public sealed class Field : MonoBehaviour
    {
        [SerializeField] private FieldScaler _fieldScaler = default;
        [SerializeField] private Transform _cellsContainer = default;
        [SerializeField] private Cell _cellPrefab = default;

        private Dictionary<Vector2Int, Cell> _cells = new Dictionary<Vector2Int, Cell>();
        private LayersBuilder _layersBuilder;
        private int _currentWidth = 0;
        private int _currentHeight = 0;

        public void ResizeField(int width, int height, LayersBuilder layersBuilder)
        {
            _layersBuilder = layersBuilder;

            if (_currentWidth == 0)
            {
                CreateCell(0, 0);
                _currentWidth = 1;
                _currentHeight = 1;
            }

            ChangeSize(_currentWidth, _currentHeight, width - _currentWidth, true);
            ChangeSize(_currentHeight, width, height - _currentHeight, false);

            _fieldScaler.Scale(width, height);
            _currentHeight = height;
            _currentWidth = width;
        }

        private void ChangeSize(int sizeMainAxis, int sizeCoAxis, int deltaMainAxis, bool horizontal)
        {
            if (deltaMainAxis == 0) return;
            if (sizeCoAxis == 0) sizeCoAxis = 1;

            for (int i = 0; i < Mathf.Abs(deltaMainAxis); i++)
            {
                for (int j = 0; j < sizeCoAxis; j++)
                {
                    ChangeCell(deltaMainAxis > 0, sizeMainAxis + i, j, horizontal);
                }
            }
        }

        private void ChangeCell(bool create, int i, int j, bool horizontal)
        {
            int x = horizontal ? i : j;
            int y = horizontal ? j : i;
            if (create) CreateCell(x, y);
            else DestroyCell(x, y);
        }

        private void CreateCell(int x, int y)
        {
            var cell = Instantiate(_cellPrefab, Vector3.zero, Quaternion.identity, _cellsContainer);
            cell.transform.localPosition = new Vector3(x, y, 0) * 1.1f;
            cell.SetLayersQueue(_layersBuilder.BuildLayersQueue());
            _cells.Add(new Vector2Int(x, y), cell);
        }

        private void DestroyCell(int x, int y)
        {
            var point = new Vector2Int(x, y);
            if (_cells.ContainsKey(point))
            {
                var cell = _cells[point];
                _cells.Remove(point);
                cell.Destroy();
            }
        }
    }
}
using System.Collections.Generic;
using Digg.Game.Builders;
using Digg.Game.Pools;
using UnityEngine;
using System;

namespace Digg.Game
{
    public sealed class Field : MonoBehaviour
    {
        [SerializeField] private FieldScaler _fieldScaler = default;
        [SerializeField] private Transform _cellsContainer = default;
        [SerializeField] private CellPool _cellsPool = default;

        private Dictionary<int, Cell> _cells = new Dictionary<int, Cell>();
        private LayersBuilder _layersBuilder;
        private int _currentWidth = 0;
        private int _currentHeight = 0;

        public void ResizeField(int width, int height, LayersBuilder layersBuilder)
        {
            _cellsPool.Initialize();

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

        public void Destroy()
        {
            _layersBuilder = null;
            _currentHeight = 0;
            _currentWidth = 0;

            foreach (var kvp in _cells)
                kvp.Value.Free();

            _cells.Clear();
        }

        private void ChangeSize(int sizeMainAxis, int sizeCoAxis, int deltaMainAxis, bool horizontal)
        {
            if (deltaMainAxis == 0) return;
            if (deltaMainAxis < 0) sizeMainAxis--;

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
            var cell = _cellsPool.BuildCell(_cellsContainer, x, y);
            cell.SetLayersQueue(_layersBuilder.BuildLayersQueue());
            _cells.Add(MathExtension.Pack(x, y), cell);
        }

        private void DestroyCell(int x, int y)
        {
            var key = MathExtension.Pack(x, y);
            if (_cells.ContainsKey(key))
            {
                var cell = _cells[key];
                _cells.Remove(key);
                cell.Free();
            }
        }
    }
}
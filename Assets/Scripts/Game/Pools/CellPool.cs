using UnityEngine;

namespace Digg.Game.Pools
{
    public sealed class CellPool : MonoBehaviour
    {
        [SerializeField] private Cell _cellPrefab = default;
        [SerializeField] private int _startCount = 35;

        private Pool<Cell> _cellsPool;

        public void Initialize()
        {
            if (_cellsPool == null)
            {
                _cellsPool = new Pool<Cell>(CreateCell, _startCount);
            }
        }

        public Cell BuildCell(Transform parent, int x, int y)
        {
            var cell = GetCell();

            cell.transform.SetParent(parent);
            cell.transform.localScale = Vector3.one;
            cell.transform.localPosition = new Vector3(x, y, 0) * 1.1f;

            cell.gameObject.name = $"Cell {x} {y}";
            return cell;
        }

        public Cell GetCell()
        {
            var cell = _cellsPool.GetObject();
            return cell;
        }

        private Cell CreateCell()
        {
            return Instantiate(_cellPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
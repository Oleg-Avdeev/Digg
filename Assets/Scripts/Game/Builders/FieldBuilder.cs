using UnityEngine;

namespace Digg.Game.Builders
{
    public sealed class FieldBuilder : MonoBehaviour
    {
        [SerializeField] private Field _field = default;
        [SerializeField] private LayersInfo _dirtInfo = default;

        public void BuildField(int width, int height, int depth)
        {
            var dirtLayersBuilder = new LayersBuilder(_dirtInfo, depth);
            _field.ResizeField(width, height, dirtLayersBuilder);
        }
    }
}
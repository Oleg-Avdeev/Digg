using Digg.Data;
using UnityEngine;

namespace Digg.Game.Builders
{
    public sealed class FieldBuilder : MonoBehaviour
    {
        [SerializeField] private Field _field = default;
        [SerializeField] private LayersInfo _dirtInfo = default;

        public void BuildField(FieldData data)
        {
            var dirtLayersBuilder = new LayersBuilder(_dirtInfo, data.Depth);
            _field.ResizeField(data.Width, data.Height, dirtLayersBuilder);
        }

        public void DestroyField()
        {
            _field.Destroy();
        }
    }
}
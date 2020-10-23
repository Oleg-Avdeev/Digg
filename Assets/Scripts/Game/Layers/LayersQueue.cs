using System.Collections.Generic;
using UnityEngine;

namespace Digg.Game.Layers
{
    // Stores Layer objects in a queue from top to bottom
    // This way you can push extra layers after some event happens

    public sealed class LayersQueue
    {
        private Queue<Layer> _layers = new Queue<Layer>();

        public void AddLayer(Layer layer)
        {
            _layers.Enqueue(layer);
        }

        public Layer GetCurrentLayer()
        {
            return _layers.Peek();
        }

        public void RemoveLayer()
        {
            _layers.Dequeue();
        }
    }
}
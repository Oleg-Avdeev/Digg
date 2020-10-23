using Digg.Game.Builders;
using UnityEngine;

namespace Digg.Game
{
    public sealed class GameController : MonoBehaviour
    {
        [SerializeField] private FieldBuilder _fieldBuilder = default;
        [SerializeField] private Chest _chest = default;

        [SerializeField] private CounterUI _treasuresCounter = default;
        [SerializeField] private CounterUI _shovelsCounter = default;

        [SerializeField] private int _shovelsCount = 0;
        [SerializeField] [Range(1, 20)] private int _fieldWidth = 5;
        [SerializeField] [Range(1, 20)] private int _fieldHeight = 5;
        [SerializeField] [Range(1, 20)] private int _fieldDepth = 10;

        private Player _player = new Player();

        private void Start() => Initialize();

        private void Initialize()
        {
            _player.OnTreasuresChange += _treasuresCounter.HandleNewValue;
            _player.OnShovelsChange += _shovelsCounter.HandleNewValue;
            
            _chest.Initialize();
            _player.Initialize(_shovelsCount);
            _fieldBuilder.BuildField(_fieldWidth, _fieldHeight, _fieldDepth);
        }

        public void BuildRandom()
        {
            _fieldWidth = Random.Range(1, 10);
            _fieldHeight = Random.Range(1, 10);
            _fieldBuilder.DestroyField();
            _fieldBuilder.BuildField(_fieldWidth, _fieldHeight, _fieldDepth);
        }

        public void GrowField()
        {
            _fieldWidth = Mathf.Min(20, _fieldWidth + 1);
            _fieldHeight = Mathf.Min(20, _fieldHeight + 1);
            _fieldBuilder.BuildField(_fieldWidth, _fieldHeight, _fieldDepth);
        }

        public void ShrinkField()
        {
            _fieldWidth = Mathf.Max(1, _fieldWidth - 1);
            _fieldHeight = Mathf.Max(1, _fieldHeight - 1);
            _fieldBuilder.BuildField(_fieldWidth, _fieldHeight, _fieldDepth);
        }
    }
}
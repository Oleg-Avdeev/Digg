using Digg.Game.Builders;
using Digg.Data;
using UnityEngine;

namespace Digg.Game
{
    public sealed class GameController : MonoBehaviour
    {
        [SerializeField] private FieldBuilder _fieldBuilder = default;
        [SerializeField] private GameObject _restartButton = default;
        [SerializeField] private Chest _chest = default;

        [SerializeField] private CounterUI _treasuresCounter = default;
        [SerializeField] private CounterUI _shovelsCounter = default;

        [SerializeField] private int _shovelsCount = 0;
        [SerializeField] private int _targetTreasures = 5;
        [SerializeField] [Range(1, 20)] private int _fieldWidth = 5;
        [SerializeField] [Range(1, 20)] private int _fieldHeight = 5;
        [SerializeField] [Range(1, 20)] private int _fieldDepth = 10;

        private Player _player = new Player();

        private void Start() => Initialize();

        private void Initialize()
        {
            // PlayerPrefs.DeleteAll();
            _player.OnTreasuresChange += _treasuresCounter.HandleNewValue;
            _player.OnShovelsChange += _shovelsCounter.HandleNewValue;
            _player.OnGameEnded += HandleGameEnded;

            _chest.Initialize();
            
            var playerData = DataManager.Instance.GetPlayerData();
            var fieldData = DataManager.Instance.GetFieldData();
            
            if (playerData != null && fieldData != null)
            {
                _player.Initialize(playerData);
                _fieldBuilder.BuildField(fieldData);
            }
            else
            {
                Restart();
            }
        }

        public void BuildRandom()
        {
            DataManager.Instance.Reset();
            
            _fieldWidth = Random.Range(1, 10);
            _fieldHeight = Random.Range(1, 10);
            _fieldBuilder.DestroyField();

            var fieldData = new FieldData(_fieldWidth, _fieldHeight, _fieldDepth);
            DataManager.Instance.SetFieldData(fieldData);
            _fieldBuilder.BuildField(fieldData);
        }

        public void GrowField()
        {
            _fieldWidth = Mathf.Min(20, _fieldWidth + 1);
            _fieldHeight = Mathf.Min(20, _fieldHeight + 1);

            var fieldData = new FieldData(_fieldWidth, _fieldHeight, _fieldDepth);
            DataManager.Instance.SetFieldData(fieldData);
            _fieldBuilder.BuildField(fieldData);
        }

        public void ShrinkField()
        {
            _fieldWidth = Mathf.Max(1, _fieldWidth - 1);
            _fieldHeight = Mathf.Max(1, _fieldHeight - 1);

            var fieldData = new FieldData(_fieldWidth, _fieldHeight, _fieldDepth);
            DataManager.Instance.SetFieldData(fieldData);
            _fieldBuilder.BuildField(fieldData);
        }

        public void Restart()
        {
            _restartButton.gameObject.SetActive(false);

            BuildRandom();

            var playerData = new PlayerData(_shovelsCount, 0, _targetTreasures);
            _player.Initialize(playerData);
            DataManager.Instance.SetPlayerData(playerData);
        }

        private void HandleGameEnded()
        {
            _restartButton.gameObject.SetActive(true);
        }
    }
}
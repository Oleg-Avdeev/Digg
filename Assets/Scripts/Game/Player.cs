using Digg.Data;
using System;

namespace Digg.Game
{
    public interface IPlayer
    {
        bool TryUseShovel();
        void AddTreasure();
    }

    public sealed class Player : IPlayer
    {
        public static IPlayer Instance { get; private set; }

        public event Action<int> OnShovelsChange;
        public event Action<int> OnTreasuresChange;
        public event Action OnGameEnded;

        private PlayerData _playerData;

        public void Initialize(PlayerData data)
        {
            Instance = this;
            _playerData = data;
            
            OnShovelsChange?.Invoke(_playerData.Shovels);
            OnTreasuresChange?.Invoke(_playerData.Treasures);

            if (_playerData.Treasures >= _playerData.TargetTreasures)
            {
                OnGameEnded?.Invoke();
            }
        }

        void IPlayer.AddTreasure()
        {
            _playerData.Treasures++;
            OnTreasuresChange?.Invoke(_playerData.Treasures);
            DataManager.Instance.SetPlayerData(_playerData);
            
            if (_playerData.Treasures >= _playerData.TargetTreasures)
            {
                OnGameEnded?.Invoke();
            }
        }

        bool IPlayer.TryUseShovel()
        {
            if (_playerData.Treasures >= _playerData.TargetTreasures)
            {
                return false;  
            }

            if (_playerData.Shovels > 0)
            {
                _playerData.Shovels--;
                OnShovelsChange?.Invoke(_playerData.Shovels);
                DataManager.Instance.SetPlayerData(_playerData);
                return true;
            }
            else
            {
                OnGameEnded?.Invoke();
                return false;
            }
        }
    }
}
using Pong.Data;
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

        private int _shovelsLeft = 0;
        private int _treasuresTarget = 0;
        private int _treasuresCounter = 0;

        public void Initialize(PlayerData data)
        {
            Instance = this;
            
            _shovelsLeft = data.Shovels;
            _treasuresCounter = data.Treasures;
            _treasuresTarget = data.TargetTreasures;
            
            OnShovelsChange?.Invoke(_shovelsLeft);
        }

        void IPlayer.AddTreasure()
        {
            _treasuresCounter++;
            OnTreasuresChange?.Invoke(_treasuresCounter);
        }

        bool IPlayer.TryUseShovel()
        {
            if (_shovelsLeft > 0)
            {
                _shovelsLeft--;
                OnShovelsChange?.Invoke(_shovelsLeft);
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}
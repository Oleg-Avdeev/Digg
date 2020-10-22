using UnityEngine;
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
        private int _treasuresCounter = 0;

        public void Initialize(int shovels)
        {
            Instance = this;
            _shovelsLeft = shovels;
            OnShovelsChange?.Invoke(shovels);
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
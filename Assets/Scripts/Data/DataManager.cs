using System.Collections.Generic;
using UnityEngine;

namespace Digg.Data
{
    public sealed class DataManager : IDataManager
    {
        private const string _fieldKey = "field{0}";
        private const string _playerKey = "player{0}";
        private const string _digsKey = "_digs{0} {1}";
        private const string _treasuresKey = "_treasures{0} {1}";

        private static IDataManager _instance;
        public static IDataManager Instance
        {
            get 
            {
                if (_instance == null)
                    _instance = new DataManager();
                return _instance;
            }
        }

        void IDataManager.SetFieldData(FieldData data)
        {
            PlayerPrefs.SetInt(string.Format(_fieldKey, 0), data.Width);
            PlayerPrefs.SetInt(string.Format(_fieldKey, 1), data.Height);
            PlayerPrefs.SetInt(string.Format(_fieldKey, 2), data.Depth);
        }

        FieldData IDataManager.GetFieldData()
        {
            var width =  PlayerPrefs.GetInt(string.Format(_fieldKey, 0), -1);
            var height =  PlayerPrefs.GetInt(string.Format(_fieldKey, 1), -1);
            var depth =  PlayerPrefs.GetInt(string.Format(_fieldKey, 2), -1);
            if (width < 0 || depth < 0 || height < 0) return null;
            return new FieldData(width, height, depth);
        }

        void IDataManager.SetPlayerData(PlayerData data)
        {
            PlayerPrefs.SetInt(string.Format(_playerKey, 0), data.Shovels);
            PlayerPrefs.SetInt(string.Format(_playerKey, 1), data.Treasures);
            PlayerPrefs.SetInt(string.Format(_playerKey, 2), data.TargetTreasures);
        }

        PlayerData IDataManager.GetPlayerData()
        {
            var shovels = PlayerPrefs.GetInt(string.Format(_playerKey, 0), -1);
            var treasures = PlayerPrefs.GetInt(string.Format(_playerKey, 1), -1);
            var targetTreasures = PlayerPrefs.GetInt(string.Format(_playerKey, 2), -1);
            if (shovels < 0 || treasures < 0 || targetTreasures < 0) return null;
            return new PlayerData(shovels, treasures, targetTreasures);
        }

        void IDataManager.AddDig(int x, int y)
        {
            var count = PlayerPrefs.GetInt(string.Format(_digsKey, x, y), 0);
            PlayerPrefs.SetInt(string.Format(_digsKey, x, y), count + 1);  
        }

        int IDataManager.GetDigsAt(int x, int y)
        {
            return PlayerPrefs.GetInt(string.Format(_digsKey, x, y), 0);
        }

        void IDataManager.AddUncoveredTreasure(int x, int y)
        {
            PlayerPrefs.SetInt(string.Format(_treasuresKey, x, y), 1);
        }

        void IDataManager.RemoveUncoveredTreasure(int x, int y)
        {
            PlayerPrefs.SetInt(string.Format(_treasuresKey, x, y), -1);
        }

        bool IDataManager.GetTreasureAt(int x, int y)
        {
            return PlayerPrefs.GetInt(string.Format(_treasuresKey, x, y), -1) != -1;
        }

        void IDataManager.Reset()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
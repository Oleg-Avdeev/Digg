using System.Collections.Generic;
using UnityEngine;

namespace Digg.Data
{
    public interface IDataManager
    {
        void SetFieldData(FieldData data);
        FieldData GetFieldData();

        void SetPlayerData(PlayerData data);
        PlayerData GetPlayerData();

        void AddDig(int x, int y);
        int GetDigsAt(int x, int y);

        void AddUncoveredTreasure(int x, int y);
        void RemoveUncoveredTreasure(int x, int y);
        bool GetTreasureAt(int x, int y);

        void Reset();
    }
}
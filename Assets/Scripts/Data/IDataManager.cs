using UnityEngine;

namespace Pong.Data
{
    public interface IDataManager
    {
        void SetFieldData(FieldData data);
        FieldData GetFieldData();

        void SetPlayerData(PlayerData data);
        PlayerData GetPlayerData();
    }
}
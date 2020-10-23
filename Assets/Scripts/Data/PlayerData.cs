
namespace Pong.Data
{
    public class PlayerData
    {
        public PlayerData(int shovels, int treasures, int targetTreasures)
        {
            Shovels = shovels;
            Treasures = treasures;
            TargetTreasures = targetTreasures;
        }

        public int Shovels;
        public int Treasures;
        public int TargetTreasures;
    }
}
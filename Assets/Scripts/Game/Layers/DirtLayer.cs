
namespace Digg.Game.Layers
{
    public sealed class DirtLayer : Layer
    {
        public override bool CanRemove()
        {
            return !_hasTreasure && base.CanRemove();
        }

        public override void Destroy()
        {
        }
    }
}
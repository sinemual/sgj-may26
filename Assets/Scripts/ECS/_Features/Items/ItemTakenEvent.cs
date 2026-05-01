using Client.Data.Equip;
using Leopotam.Ecs;

namespace Client.ECS.CurrentGame.Loot.Components
{
    public struct ItemTakenEvent
    {
        public EcsEntity Owner;
        public ItemData Value;
    }
}
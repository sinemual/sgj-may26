using System;

namespace Client.ECS.CurrentGame.Player
{
    [Serializable]
    public class StatModifier
    {
        public float Value;
        public StatModifierType Type;
        public int Order;
        public IStatModifierSource Source;
    }
}
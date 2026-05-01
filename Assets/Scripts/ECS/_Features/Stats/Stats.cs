using System.Collections.Generic;
using Client.Data.Core;
using Client.ECS.CurrentGame.Player;

namespace Assets.Scripts.ECS._Features.Stats
{
    public struct Stats 
    {
        public Dictionary<StatType, Stat> Value;
    }
}
using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

namespace Client
{
    public class WinSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private UserInterface _userInterface;

        private EcsFilter<WinEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                _userInterface.ShowScreen<WinScreen>();
            }
        }
    }
}
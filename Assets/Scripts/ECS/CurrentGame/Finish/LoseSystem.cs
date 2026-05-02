using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

namespace Client
{
    public class LoseSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private UserInterface _userInterface;

        private EcsFilter<LoseEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                _data.RuntimeData.RaceStep = 0;
                _userInterface.ShowScreen<LoseScreen>();
            }
        }
    }
}
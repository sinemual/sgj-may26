using Client.Data;
using Client.Data.Core;
using Client.ECS.CurrentGame.Hit.Systems;
using Client.Factories;
using Extensions;
using Leopotam.Ecs;

namespace Client
{
    public class CaviarSpawnSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<CaviarSpawnProvider>.Exclude<Timer<ReloadingTimer>> _filter;

        public void Run()
        {
            if(_data.RuntimeData.CurrentGameStateType != GameStateType.CatchingStep)
                return;
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var spawnPoints = ref entity.Get<CaviarSpawnProvider>().SpawnPoints;

                foreach (var point in spawnPoints)
                {
                    var data = _data.StaticData.TadpoleData.GetRandom();
                    EcsEntity caviarEntity = _prefabFactory.Spawn(data.Prefab, point.position, point.rotation);
                    caviarEntity.Get<TadpoleDataComponent>().Value = data;
                }

                entity.Get<Timer<ReloadingTimer>>().Value = 1.0f;
            }
        }
    }
}
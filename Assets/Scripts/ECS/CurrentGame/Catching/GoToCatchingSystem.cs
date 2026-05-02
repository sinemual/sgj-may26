using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;
using CameraType = Client.Data.CameraType;

namespace Client
{
    public class GoToCatchingSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private CameraService _cameraService;

        private EcsFilter<GoToCatchingRequest>.Exclude<DespawnLevelRequest> _filter;
        private EcsFilter<CurrentStepMarker> _currentStepFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);

                _prefabFactory.Despawn(ref _currentStepFilter.GetEntity(0));
                EcsEntity stepEntity = 
                    _prefabFactory.Spawn(_data.StaticData.PrefabData.CatchingStepPrefab, Vector3.zero, Quaternion.identity);
                stepEntity.Get<CurrentStepMarker>();
                _prefabFactory.SetDefaultParent(stepEntity.Get<GameObjectProvider>().Value.transform);
                _cameraService.SetCamera(CameraType.Catching, isWarp: true);

                var spawnPoint = stepEntity.Get<CathcingStepProvider>().TrapSpawnPoint;
                EcsEntity trapEntity = 
                    _prefabFactory.Spawn(_data.StaticData.PrefabData.TrapPrefab, spawnPoint.position, Quaternion.identity);
                
                entity.Del<GoToCatchingRequest>();
            }
        }
    }
}
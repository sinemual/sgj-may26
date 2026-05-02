using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;
using CameraType = Client.Data.CameraType;

namespace Client
{
    public class GoToGatheringSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private CameraService _cameraService;
        
        private EcsFilter<GoToGatheringRequest>.Exclude<DespawnLevelRequest> _filter;
        private EcsFilter<CurrentStepMarker> _currentStepFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);

                _prefabFactory.Despawn(ref _currentStepFilter.GetEntity(0));
                EcsEntity stepEntity = _prefabFactory.Spawn(_data.StaticData.PrefabData.GatheringStepPrefab, Vector3.zero, Quaternion.identity);
                stepEntity.Get<CurrentStepMarker>();
                _prefabFactory.SetDefaultParent(stepEntity.Get<GameObjectProvider>().Value.transform);
                _cameraService.SetCamera(CameraType.Gathering, isWarp: true );
                
                entity.Del<GoToGatheringRequest>();
            }
        }
    }

    internal struct CurrentStepMarker : IEcsIgnoreInFilter
    {
    }
}
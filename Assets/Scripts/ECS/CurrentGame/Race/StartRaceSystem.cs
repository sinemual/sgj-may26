using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;
using CameraType = Client.Data.CameraType;

namespace Client
{
    public class StartRaceSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private CameraService _cameraService;

        private EcsFilter<StartRaceRequest>.Exclude<DespawnLevelRequest> _filter;
        private EcsFilter<TadpoleProvider, PlayerTag> _playerFilter;
        private EcsFilter<CurrentStepMarker> _currentStepFilter;
        

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);

                if (_data.RuntimeData.CurrentTadpole == -1 || _data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].MetamorphosisStep == 0)
                {
                    entity.Del<StartRaceRequest>();
                    continue;
                }

                _prefabFactory.Despawn(ref _currentStepFilter.GetEntity(0));
                EcsEntity stepEntity =_prefabFactory.Spawn(_data.StaticData.Levels[(int)_data.RuntimeData.RaceStep].Prefab,
                    Vector3.zero, Quaternion.identity);
                stepEntity.Get<CurrentStepMarker>();
                _prefabFactory.SetDefaultParent(stepEntity.Get<GameObjectProvider>().Value.transform);
                _cameraService.SetCamera(CameraType.Race, isWarp: true);
                entity.Del<StartRaceRequest>();
            }
        }
    }
}
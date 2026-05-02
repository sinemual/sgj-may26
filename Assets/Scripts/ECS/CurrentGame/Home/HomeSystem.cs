using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class HomeSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<HomeProvider>.Exclude<InitedMarker> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var spawnPoints = ref entity.Get<HomeProvider>().SpawnPoints;
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;

                for (int i = 0; i < _data.SaveData.TadpoleByJar.Length; i++)
                {
                    if (_data.SaveData.TadpoleByJar[i] != -1)
                    {
                        var playerData =
                            _data.StaticData.TadpoleDataByType[_data.SaveData.TadpoleSaveData[_data.SaveData.TadpoleByJar[i]].TadpoleType];
                        EcsEntity playerEntity = _prefabFactory.Spawn(playerData.Prefab, spawnPoints[i].position, spawnPoints[i].rotation, entityGo.transform);
                        playerEntity.Get<TadpoleDataComponent>().Value = playerData;
                        playerEntity.Get<SaveId>().Value = _data.SaveData.TadpoleByJar[i];
                        playerEntity.Get<RigidbodyProvider>().Value.isKinematic = true;
                    }
                }
                
                entity.Get<InitedMarker>();
            }
        }
    }
}
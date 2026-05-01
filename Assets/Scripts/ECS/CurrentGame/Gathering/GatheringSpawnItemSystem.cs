using Client.Data.Core;
using Client.ECS.CurrentGame.Equipment;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class GatheringSpawnItemSystem : IEcsInitSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<GatheringSpawnItemProvider> _filter;

        public void Init()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var spawnPoints = ref entity.Get<GatheringSpawnItemProvider>().SpawnPoints;

                foreach (var spawnPoint in spawnPoints)
                {
                    if (Random.value > 0.0f)
                    {
                        float random = Random.value;

                        foreach (var item in _data.StaticData.ItemData)
                        {
                            if (random >= _data.BalanceData.SpawnItemChance[item.ItemType])
                            {
                                EcsEntity spawnItemEntity = _prefabFactory.Spawn(item.ItemView.ItemPrefab, spawnPoint.position, Quaternion.identity);
                                spawnItemEntity.Get<ItemDataComponent>().Value = item;
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
using Client.Data.Core;
using Client.ECS.CurrentGame.Equipment;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class GatheringSpawnItemSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<GatheringSpawnItemProvider>.Exclude<InitedMarker> _filter;

        public void Run()
        {
            if (_data.RuntimeData.IsTodayGathered)
                return;

            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var spawnPoints = ref entity.Get<GatheringSpawnItemProvider>().SpawnPoints;

                foreach (var spawnPoint in spawnPoints)
                {
                    //Debug.Log($"{Random.value}");
                    if (Random.value > 0.2f)
                    {
                        float random = Random.value;

                        var revertedList = _data.StaticData.ItemData;
                        revertedList.Reverse();

                        foreach (var item in revertedList)
                        {
                            //Debug.Log($"item {item.IngredientType} >= {random} >= {_data.BalanceData.SpawnIngredientChanceByType[item.IngredientType]}");
                            if (random >= _data.BalanceData.SpawnIngredientChanceByType[item.IngredientType])
                            {
                                //Debug.Log($"{item.IngredientType}");
                                EcsEntity spawnItemEntity = _prefabFactory.Spawn(item.ItemView.ItemPrefab, spawnPoint.position, spawnPoint.rotation);
                                spawnItemEntity.Get<ItemDataComponent>().Value = item;
                                spawnItemEntity.Get<GameObjectProvider>().Value.transform.localScale = Vector3.one * 2.0f;
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                _data.RuntimeData.IsTodayGathered = true;
                entity.Get<InitedMarker>();
            }
        }
    }
}
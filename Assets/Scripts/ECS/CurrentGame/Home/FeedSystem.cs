using Client.Data;
using Client.Data.Core;
using Client.ECS.CurrentGame.Equipment;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class FeedSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private AudioService _audioService;

        private EcsFilter<FeedRequest> _filter;
        private EcsFilter<TadpoleProvider> _tadpoleFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);

                if (_data.RuntimeData.CurrentTadpole == -1)
                {
                    entity.Del<FeedRequest>();
                    continue;
                }

                _audioService.Play(Sounds.PopSound);
                _data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].FatAmount += 0.1f;
                _data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].IsFed = true;

                _prefabFactory.Spawn(_data.StaticData.PrefabData.FoodPrefab,
                    _data.SceneData.SpawnFoodPoint.position, Quaternion.identity);

                entity.Del<FeedRequest>();
            }
        }
    }
}
using Client.Data;
using Client.Data.Core;
using Client.ECS.CurrentGame.Equipment;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class GatheringSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private AudioService _audioService;

        private EcsFilter<GatheringMarker, MovingCompleteEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var date = ref entity.Get<ItemDataComponent>().Value;

                _audioService.Play(Sounds.PopSound);
                _data.SaveData.Ingredients[date.IngredientType] += 1; // Random.Range(1, 4);
                _prefabFactory.Despawn(ref entity);
            }
        }
    }
}
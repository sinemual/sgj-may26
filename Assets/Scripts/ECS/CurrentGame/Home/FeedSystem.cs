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

        private EcsFilter<FeedRequest> _filter;
        private EcsFilter<TadpoleProvider> _tadpoleFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);

                _data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].Fat += 1;
                _data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].IsFed = true;
                
                entity.Del<FeedRequest>();
            }
        }
    }
}
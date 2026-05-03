using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class FeedDestroySystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private AudioService _audioService;

        private EcsFilter<FeedTagProvider> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var feed = ref entity.Get<GameObjectProvider>().Value;

                if (feed.transform.position.y < 2.8f)
                {
                    _audioService.Play(Sounds.ChawSound);
                    _prefabFactory.Despawn(ref entity);
                }
            }
        }
    }
}
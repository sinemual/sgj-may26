using Client.Data.Core;
using Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class InitTadpoleSystem : IEcsRunSystem
    {
        private SharedData _data;
        private CameraService _cameraService;
        private AudioService _audioService;

        private EcsFilter<TadpoleProvider>.Exclude<InitedMarker> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;
                ref var animator = ref entity.Get<AnimatorProvider>().Value;
                ref var entityRb = ref entity.Get<RigidbodyProvider>().Value;

                entity.Get<CurrentPoint>().Value = 0;
                
                entity.Get<InitedMarker>();
            }
        }
    }
}
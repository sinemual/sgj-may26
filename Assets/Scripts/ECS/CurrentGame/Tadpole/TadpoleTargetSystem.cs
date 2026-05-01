using Client.Data.Core;
using Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TadpoleTargetSystem : IEcsRunSystem
    {
        private SharedData _data;
        private CameraService _cameraService;
        private AudioService _audioService;

        private EcsFilter<TadpoleProvider>.Exclude<Timer<ThinkingTimer>> _filter;
        private EcsFilter<GoRequest> _goFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;
                ref var animator = ref entity.Get<AnimatorProvider>().Value;
                ref var entityRb = ref entity.Get<RigidbodyProvider>().Value;

                Vector3 random = Random.onUnitSphere * _data.BalanceData.RandomTargetRadius;
                random = random.SetY(0.0f);
                Vector3 target = entityGo.transform.position + random;

                foreach (var idz in _goFilter)
                {
                    ref var goEventEntity = ref _goFilter.GetEntity(idz);
                    ref var goEventPosition = ref goEventEntity.Get<GoRequest>().Position;
                    target = goEventPosition;
                    goEventEntity.Del<GoRequest>();
                }

                entity.Get<Target>().Value = target;
                entity.Get<Timer<ThinkingTimer>>().Value = _data.BalanceData.ThinkingTime;
                _data.SceneData.Target.position = target;
            }
        }
    }
    
    public class FinishSystem : IEcsRunSystem
    {
        private SharedData _data;
        private CameraService _cameraService;
        private AudioService _audioService;

        private EcsFilter<TadpoleProvider>.Exclude<Timer<ThinkingTimer>> _filter;
        private EcsFilter<GoRequest> _goFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;
                ref var animator = ref entity.Get<AnimatorProvider>().Value;
                ref var entityRb = ref entity.Get<RigidbodyProvider>().Value;

                Vector3 random = Random.onUnitSphere * _data.BalanceData.RandomTargetRadius;
                random = random.SetY(0.0f);
                Vector3 target = entityGo.transform.position + random;

                foreach (var idz in _goFilter)
                {
                    ref var goEventEntity = ref _goFilter.GetEntity(idz);
                    ref var goEventPosition = ref goEventEntity.Get<GoRequest>().Position;
                    target = goEventPosition;
                    goEventEntity.Del<GoRequest>();
                }

                entity.Get<Target>().Value = target;
                entity.Get<Timer<ThinkingTimer>>().Value = _data.BalanceData.ThinkingTime;
                _data.SceneData.Target.position = target;
            }
        }
    }
}
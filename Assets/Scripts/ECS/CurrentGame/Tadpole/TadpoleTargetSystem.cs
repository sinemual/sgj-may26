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

                if (entity.Has<GoRequest>())
                {
                    ref var goEventPosition = ref entity.Get<GoRequest>().Position;
                    target = goEventPosition;
                    entity.Del<GoRequest>();
                }

                entity.Get<Target>().Value = target;
                entity.Get<Timer<ThinkingTimer>>().Value = _data.BalanceData.ThinkingTime;

                if (entity.Has<PlayerTagProvider>()) // debug
                    _data.SceneData.RealTarget.position = target;
            }
        }
    }

    public struct CurrentPoint
    {
        public int Value;
    }
}
using System.Collections.Generic;
using Assets.Scripts.ECS._Features.Stats;
using Client.Data.Core;
using Client.ECS.CurrentGame.Player;
using Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TadpoleThinkingSystem : IEcsRunSystem
    {
        private SharedData _data;
        private CameraService _cameraService;
        private AudioService _audioService;

        private EcsFilter<TadpoleProvider>.Exclude<Timer<ThinkingTimer>> _filter;
        private EcsFilter<RaceManagerProvider, InitedMarker> _raceFilter;

        public void Run()
        {
            if(_raceFilter.IsEmpty())
                return;
            
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;
                ref var animator = ref entity.Get<AnimatorProvider>().Value;
                ref var entityRb = ref entity.Get<RigidbodyProvider>().Value;
                ref var stats = ref entity.Get<Stats>().Value;

                Vector3 random = Random.onUnitSphere * _data.BalanceData.RandomTargetRadius;
                random = random.SetY(0.0f);
                Vector3 target = entityGo.transform.position + random;

                if (entity.Has<LureRequest>())
                {
                    ref var goEventPosition = ref entity.Get<LureRequest>().Position;
                    target = goEventPosition;
                    entity.Del<LureRequest>();
                }

                entity.Get<Target>().Value = target;
                entity.Get<Timer<ThinkingTimer>>().Value = (1.0f - stats[StatType.Intelligence].GetValue()) * _data.BalanceData.ThinkingTimeMultiplier;

                if (entity.Has<PlayerTag>()) // debug
                    _data.SceneData.RealTarget.position = target;
            }
        }
    }

    public struct CurrentPoint
    {
        public int Value;
    }
}
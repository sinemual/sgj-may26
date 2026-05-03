using Assets.Scripts.ECS._Features.Stats;
using Client.Data;
using Client.Data.Core;
using Client.ECS.CurrentGame.Hit.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TadpoleDamageSystem : IEcsRunSystem
    {
        private SharedData _data;
        private CameraService _cameraService;
        private AudioService _audioService;

        private EcsFilter<TadpoleProvider, RigidbodyProvider, OnCollisionEnterEvent> _filter;
        private EcsFilter<RaceManagerProvider, InitedMarker> _raceFilter;

        public void Run()
        {
            if(_raceFilter.IsEmpty())
                return;
            
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;
                ref var entityRb = ref entity.Get<RigidbodyProvider>().Value;
                ref var target = ref entity.Get<Target>().Value;
                ref var stats = ref entity.Get<Stats>().Value;
                ref var health = ref entity.Get<Health>();
                ref var animator = ref entity.Get<AnimatorProvider>().Value;

                if (entityRb.linearVelocity.magnitude > _data.BalanceData.DamageSpeedTrasholdValue)
                {
                    Debug.Log($"entityRb.linearVelocity.magnitude: {entityRb.linearVelocity.magnitude}");
                    health.Value -= (entityRb.linearVelocity.magnitude * _data.BalanceData.DamageSpeedMultiplier) - stats[StatType.Armor].GetValue();
                    animator.SetTrigger(Animations.IsTuk);
                    entityRb.linearVelocity = Vector3.zero;
                }
            }
        }
    }
}
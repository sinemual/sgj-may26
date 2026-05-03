using Assets.Scripts.ECS._Features.Stats;
using Client.Data.Core;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TadpoleRotationSystem : IEcsRunSystem
    {
        private SharedData _data;
        private CameraService _cameraService;
        private AudioService _audioService;

        private EcsFilter<TadpoleProvider, RigidbodyProvider, Target>.Exclude<DeadState> _filter;
        private EcsFilter<RaceManagerProvider, InitedMarker> _raceFilter;

        public void Run()
        {
            if (_raceFilter.IsEmpty())
                return;

            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;
                ref var animator = ref entity.Get<AnimatorProvider>().Value;
                ref var entityRb = ref entity.Get<RigidbodyProvider>().Value;
                ref var target = ref entity.Get<Target>().Value;
                ref var stats = ref entity.Get<Stats>().Value;

                Vector3 moveDirection = (target - entityGo.transform.position).normalized;
                
                if (entityRb.linearVelocity.magnitude > 0.1f)
                {
                    var fatPenalty = stats[StatType.Fat].GetValue() *_data.BalanceData.FatPenaltyMultiplier;
                    var rotateSpeed = stats[StatType.Speed].GetValue() * _data.BalanceData.RotateSpeedMultiplier - fatPenalty;

                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                    entityRb.MoveRotation(Quaternion.RotateTowards(entityRb.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime));
                }
            }
        }
    }
}
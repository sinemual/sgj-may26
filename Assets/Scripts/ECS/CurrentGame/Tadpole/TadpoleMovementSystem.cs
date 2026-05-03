using Assets.Scripts.ECS._Features.Stats;
using Client.Data;
using Client.Data.Core;
using Client.ECS.CurrentGame.Hit.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TadpoleMovementSystem : IEcsRunSystem
    {
        private SharedData _data;
        private CameraService _cameraService;
        private AudioService _audioService;

        private EcsFilter<TadpoleProvider, RigidbodyProvider, Target>.Exclude<Timer<ReloadingTimer>, DeadState> _filter;
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
                ref var target = ref entity.Get<Target>().Value;
                ref var stats = ref entity.Get<Stats>().Value;


                var fatPenalty = stats[StatType.Fat].GetValue() * _data.BalanceData.FatPenaltyMultiplier;
                var speed = stats[StatType.Speed].GetValue() * _data.BalanceData.MoveSpeedMultiplier - fatPenalty;
                animator.SetFloat(Animations.MovementSpeed, speed);
                entityRb.linearVelocity = Vector3.zero;
                entityRb.AddForce(entityGo.transform.forward * speed, ForceMode.Impulse);
                //entityRb.linearVelocity = entityGo.transform.forward;
                entity.Get<Timer<ReloadingTimer>>().Value =_data.BalanceData.ReloadingMovementTime;
            }
        }
    }
}
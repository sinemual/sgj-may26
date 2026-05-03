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
                //Debug.Log($"speed {speed}");
                animator.SetFloat(Animations.MovementSpeed, speed);
                entityRb.linearVelocity = Vector3.zero;
                entityRb.AddForce(entityGo.transform.forward * speed, ForceMode.Impulse);
                //entityRb.linearVelocity = entityGo.transform.forward;
                entity.Get<Timer<ReloadingTimer>>().Value =_data.BalanceData.ReloadingMovementTime;
            }
        }
    }
    
    public class TadpoleMovementCheckerSystem : IEcsRunSystem
    {
        private SharedData _data;
        private CameraService _cameraService;
        private AudioService _audioService;

        private EcsFilter<TadpoleProvider, RigidbodyProvider, Target>.Exclude<Timer<CheckTimer>, DeadState> _filter;
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

                if (entity.Has<LastPosition>())
                {
                    if(Vector3.Distance(entity.Get<LastPosition>().Value, entityGo.transform.position) < 1.0f)
                        entityRb.AddForce(-entityGo.transform.forward * 10.0f, ForceMode.Impulse);
                }
                entity.Get<Timer<CheckTimer>>().Value = 5.0f;
                entity.Get<LastPosition>().Value = entityGo.transform.position;
            }
        }
    }

    public struct LastPosition
    {
        public Vector3 Value;
    }

    internal struct CheckTimer
    {
        public float Value;
    }
}
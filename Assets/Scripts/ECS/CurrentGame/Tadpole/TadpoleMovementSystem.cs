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

        private EcsFilter<TadpoleProvider, RigidbodyProvider, Target>.Exclude<Timer<ReloadingTimer>> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;
                ref var animator = ref entity.Get<AnimatorProvider>().Value;
                ref var entityRb = ref entity.Get<RigidbodyProvider>().Value;
                ref var target = ref entity.Get<Target>().Value;

                //animator.SetBool(Animations.IsRun, false);
                entityRb.AddForce(entityGo.transform.forward * _data.BalanceData.DebugSpeed, ForceMode.Force);
                //entityRb.linearVelocity = entityGo.transform.forward;
                entity.Get<Timer<ReloadingTimer>>().Value =_data.BalanceData.ReloadingMovementTime;
            }
        }
    }
}
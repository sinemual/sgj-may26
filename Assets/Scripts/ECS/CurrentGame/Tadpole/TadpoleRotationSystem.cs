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

        private EcsFilter<TadpoleProvider, RigidbodyProvider, Target> _filter;
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

                //animator.SetBool(Animations.IsRun, false);

                Vector3 moveDirection = (target - entityGo.transform.position).normalized;
                float speed = 1;
                Vector3 velocity = moveDirection * speed;
                //velocity.y = entityRb.linearVelocity.y;
                //entityRb.linearVelocity = velocity;

                //animator.SetFloat(Animations.MovementSpeed, speed * 0.5f);
                if (velocity.sqrMagnitude > 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                    entityRb.MoveRotation(Quaternion.RotateTowards(entityRb.rotation, targetRotation,
                        _data.BalanceData.CharacterRotateSpeed * Time.fixedDeltaTime));
                }
            }
        }
    }
}
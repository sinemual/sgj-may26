using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class VelocityPositionMovingSystem : IEcsRunSystem
    {
        private EcsFilter<VelocityPositionMoving> _movingFilter;

        public void Run()
        {
            if (_movingFilter.IsEmpty())
                return;

            foreach (var movingObject in _movingFilter)
            {
                ref var movingEntity = ref _movingFilter.GetEntity(movingObject);
                ref var movingEntityGo = ref movingEntity.Get<GameObjectProvider>().Value;
                ref var movingEntityRb = ref movingEntity.Get<RigidbodyProvider>().Value;

                ref var moving = ref movingEntity.Get<VelocityPositionMoving>();

                moving.Speed = moving.Speed == 0 ? 2 : moving.Speed;
                moving.Accuracy = moving.Accuracy == 0 ? 0.1f : moving.Accuracy;

                movingEntityRb.linearVelocity +=
                    (moving.Target - movingEntityGo.transform.position).normalized * (moving.Speed * Time.deltaTime);

                if (Vector3.Distance(movingEntityGo.transform.position, moving.Target) <
                    moving.Accuracy)
                {
                    movingEntityRb.linearVelocity = Vector3.zero;
                    movingEntity.Del<VelocityPositionMoving>();
                    movingEntity.Get<MovingCompleteEvent>();
                }
            }
        }
    }
}
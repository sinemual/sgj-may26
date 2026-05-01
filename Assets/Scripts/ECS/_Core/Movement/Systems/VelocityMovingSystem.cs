using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class VelocityMovingSystem : IEcsRunSystem
    {
        private EcsFilter<VelocityMoving> _movingFilter;

        public void Run()
        {
            if (_movingFilter.IsEmpty())
                return;

            foreach (var movingObject in _movingFilter)
            {
                ref var movingEntity = ref _movingFilter.GetEntity(movingObject);
                ref var movingEntityGo = ref movingEntity.Get<GameObjectProvider>().Value;
                ref var movingEntityRb = ref movingEntity.Get<RigidbodyProvider>().Value;

                ref var moving = ref movingEntity.Get<VelocityMoving>();

                moving.Speed = moving.Speed == 0 ? 2 : moving.Speed;
                moving.Accuracy = moving.Accuracy == 0 ? 0.1f : moving.Accuracy;

                movingEntityRb.linearVelocity +=
                    (moving.Target.position + moving.Offset - movingEntityGo.transform.position).normalized * (moving.Speed * Time.deltaTime);

                if (Vector3.Distance(movingEntityGo.transform.position, moving.Target.position + moving.Offset) <
                    moving.Accuracy)
                {
                    movingEntityRb.linearVelocity = Vector3.zero;
                    movingEntity.Del<VelocityMoving>();
                    movingEntity.Get<MovingCompleteEvent>();
                }
            }
        }
    }
}
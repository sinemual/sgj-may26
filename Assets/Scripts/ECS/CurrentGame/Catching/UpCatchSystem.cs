using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class UpCatchSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<RaycastEvent> _filter;
        private EcsFilter<CatchProvider, InPuddleState>.Exclude<VelocityPositionMoving> _catchfilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var raycastEvent = ref entity.Get<RaycastEvent>();

                if (raycastEvent.GameObject.CompareTag(_data.StaticData.PuddleTag))
                {
                    foreach (var idz in _catchfilter)
                    {
                        ref var catchEntity = ref _catchfilter.GetEntity(idz);
                        ref var catchGo = ref catchEntity.Get<GameObjectProvider>().Value;

                        if (Vector3.Distance(raycastEvent.HitPoint, catchGo.transform.position) < 3.0f)
                        {
                            catchEntity.Get<VelocityPositionMoving>() = new VelocityPositionMoving()
                            {
                                Target = catchGo.transform.position + Vector3.up * 5.0f,
                                Speed = _data.BalanceData.DropItemSpeed,
                                Accuracy = 1.0f
                            };
                        }
                    }
                }
            }
        }
    }
}
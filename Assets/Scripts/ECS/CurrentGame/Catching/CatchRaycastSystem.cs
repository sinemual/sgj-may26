using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class CatchRaycastSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<RaycastEvent> _filter;
        private EcsFilter<TrapProvider>.Exclude<InPuddleState> _catchfilter;

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
                        ref var catchRb = ref catchEntity.Get<RigidbodyProvider>().Value;

                        catchGo.transform.position = raycastEvent.HitPoint.SetY(5.0f);
                        catchRb.isKinematic = false;
                        catchEntity.Get<InPuddleState>();
                    }
                    /*_prefabFactory.Spawn(_data.StaticData.PrefabData.Catch)
                    _world.NewEntity().Get<PlaceCatchRequest>().Position = ;*/
                }
            }
        }
    }
}
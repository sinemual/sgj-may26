using Client.Data.Core;
using Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class RaycastToGoEventSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        
        private EcsFilter<RaycastEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var raycastEvent = ref entity.Get<RaycastEvent>();

                if (raycastEvent.GameObject.CompareTag(_data.StaticData.CoastTag))
                {
                    _world.NewEntity().Get<GoEvent>().Position = raycastEvent.HitPoint.SetY(0);
                    _world.NewEntity().Get<GoRequest>().Position = raycastEvent.HitPoint.SetY(0);
                }
            }
        }
    }

    public struct GoRequest
    {
        public Vector3 Position;
    }
}
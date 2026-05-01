using Client.Data.Core;
using Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class LureRaycastSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;

        private EcsFilter<RaycastEvent> _filter;
        private EcsFilter<TadpoleProvider, PlayerTagProvider> _playerFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var raycastEvent = ref entity.Get<RaycastEvent>();

                if (raycastEvent.GameObject.CompareTag(_data.StaticData.CoastTag))
                {
                    _world.NewEntity().Get<LureEvent>().Position = raycastEvent.HitPoint.SetY(0);

                    foreach (var idz in _playerFilter)
                    {
                        ref var playerEntity = ref _playerFilter.GetEntity(idz);

                        if (raycastEvent.GameObject.CompareTag(_data.StaticData.CoastTag))
                        {
                            playerEntity.Get<LureRequest>().Position = raycastEvent.HitPoint.SetY(0);
                            _data.SceneData.PlayerTarget.position = raycastEvent.HitPoint.SetY(0);
                        }
                    }
                }
            }
        }
    }

    /*public struct PlaceCatchRequest
    {
        public Vector3 Position;
    }*/

    public struct LureRequest
    {
        public Vector3 Position;
    }
}
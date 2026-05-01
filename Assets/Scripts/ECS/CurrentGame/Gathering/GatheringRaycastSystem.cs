using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;

namespace Client
{
    public class GatheringRaycastSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<RaycastEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var raycastEvent = ref entity.Get<RaycastEvent>();

                if (raycastEvent.GameObject.TryGetComponent(out MonoEntity monoEntity))
                {
                    if (monoEntity.Entity.Has<GatheringItemProvider>())
                    {
                        monoEntity.Entity.Get<GatheringMarker>();
                        monoEntity.Entity.Get<TransformAroundMoving>() = new TransformAroundMoving()
                        {
                            Target = _data.SceneData.CatchPoint,
                            Radius = 5.0f
                        };
                    }
                }
            }
        }
    }
}
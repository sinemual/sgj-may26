using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;

namespace Client
{
    public class TryCatchSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<CatchProvider, InPuddleState, MovingCompleteEvent> _catchfilter;
        private EcsFilter<CaviarProvider> _caviarFilter;

        public void Run()
        {
            foreach (var idx in _catchfilter)
            {
                ref var entity = ref _catchfilter.GetEntity(idx);
                ref var catchGo = ref entity.Get<GameObjectProvider>().Value;
                ref var catchRb = ref entity.Get<RigidbodyProvider>().Value;

                foreach (var idz in _caviarFilter)
                {
                    ref var caviarEntity = ref _caviarFilter.GetEntity(idz);
                    ref var caviar = ref caviarEntity.Get<CaviarProvider>();
                    ref var caviarGo = ref caviarEntity.Get<GameObjectProvider>().Value;

                    if (caviarGo.transform.position.y > catchGo.transform.position.y)
                    {
                        caviarEntity.Get<CatchRequest>();
                        caviarEntity.Get<TransformAroundMoving>() = new TransformAroundMoving()
                        {
                            Target = _data.SceneData.CatchPoint,
                            Radius = 5.0f
                        };
                    }
                }

                catchRb.isKinematic = true;
                entity.Del<InPuddleState>();
            }
        }
    }
}
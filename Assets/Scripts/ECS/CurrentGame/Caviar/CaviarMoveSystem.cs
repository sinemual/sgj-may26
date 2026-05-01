using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class CaviarMoveSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<CaviarProvider> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var entityRb = ref entity.Get<RigidbodyProvider>().Value;

                entityRb.AddForce(Vector3.right * _data.BalanceData.CaviarMoveSpeed + Vector3.forward * Random.Range(-0.5f, 0.5f), ForceMode.Force);
                if (entityRb.position.y < 0.4f)
                    entityRb.AddForce(Vector3.up * 18.0f, ForceMode.Force);
                if (entityRb.position.x >= 20.0f)
                    _prefabFactory.Despawn(ref entity);
            }
        }
    }
}
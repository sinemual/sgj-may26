using Client.Data.Core;
using Client.DevTools.MyTools;
using Client.Factories;
using Data;
using Leopotam.Ecs;

namespace Client
{
    public class CaviarCatchSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<CaviarProvider, CatchRequest, MovingCompleteEvent> _caviarFilter;

        public void Run()
        {
            foreach (var idz in _caviarFilter)
            {
                ref var caviarEntity = ref _caviarFilter.GetEntity(idz);
                ref var caviar = ref caviarEntity.Get<CaviarProvider>();
                ref var caviarGo = ref caviarEntity.Get<GameObjectProvider>().Value;
                ref var data = ref caviarEntity.Get<TadpoleDataComponent>().Value;

                _data.SaveData.TadpoleSaveData.Add(_data.SaveData.CatchCounter.ToString(), new TadpoleSaveData()
                {
                    DataId = data.Id,
                    TadpoleName =  Names.GetRandom()
                });
                
                _data.SaveData.CatchCounter += 1;
                
                
                _prefabFactory.Despawn(ref caviarEntity);
            }
        }
    }
}
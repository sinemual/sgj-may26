using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

namespace Client
{
    public class FlushSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private AudioService _audioService;
        private UserInterface _ui;

        private EcsFilter<FlushRequest> _filter;
        private EcsFilter<TadpoleProvider> _tadpoleFilter;
        private EcsFilter<HomeProvider,InitedMarker> _homeFilter;

        public void Run()
        {
            if(_homeFilter.IsEmpty())
                return;
            
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);

                if (_data.SaveData.TadpoleByJar[_data.RuntimeData.CurrentHomePoint] != -1)
                {
                    _data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].IsDead = true;
                    _data.SaveData.TadpoleByJar[_data.RuntimeData.CurrentHomePoint] = -1;

                    foreach (var idz in _tadpoleFilter)
                    {
                        ref var tadpoleEntity = ref _tadpoleFilter.GetEntity(idz);
                        ref var saveId = ref tadpoleEntity.Get<SaveId>().Value;

                        if (saveId == _data.RuntimeData.CurrentTadpole)
                        {
                            _prefabFactory.Despawn(ref tadpoleEntity);
                            _ui.GetScreen<HomeScreen>().UpdateTadpoleNameText("Empty");
                            _ui.GetScreen<HomeScreen>().UpdateNumberText(" ");
                        }
                    }

                    _ui.GetScreen<GameScreen>().Sleep();
                    _audioService.Play(Sounds.FlushSound);
                }


                entity.Del<FlushRequest>();
            }
        }
    }
}
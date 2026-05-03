using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

namespace Client
{
    public class SleepSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private UserInterface _ui;

        private EcsFilter<SleepRequest> _filter;
        private EcsFilter<TadpoleProvider> _tadpoleFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);

                _data.SaveData.Day += 1;
                _ui.GetScreen<GameScreen>().Sleep();

                for (int i = 0; i < _data.SaveData.TadpoleByJar.Length; i++)
                {
                    if (_data.SaveData.TadpoleByJar[i] != -1)
                    {
                        var data = _data.SaveData.TadpoleSaveData[_data.SaveData.TadpoleByJar[i]];
                        if (!data.IsFed)
                        {
                            data.IsDead = true;
                        }
                        else
                        {
                            data.MetamorphosisStep += 1;
                        }
                    }
                }

                foreach (var idz in _tadpoleFilter)
                {
                    ref var tadpoleEntity = ref _tadpoleFilter.GetEntity(idz);
                    tadpoleEntity.Get<UpdateStateRequest>();
                }
                
                _ui.GetScreen<GameScreen>().UpdateDayText(_data.SaveData.Day);

                entity.Del<SleepRequest>();
            }
        }
    }

    public struct UpdateStateRequest : IEcsIgnoreInFilter
    {
    }
}
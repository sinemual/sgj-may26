using System.Collections.Generic;
using Client.Data;
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
        private AudioService _audioService;

        private EcsFilter<CaviarProvider, CatchRequest, MovingCompleteEvent> _caviarFilter;

        public void Run()
        {
            foreach (var idz in _caviarFilter)
            {
                ref var caviarEntity = ref _caviarFilter.GetEntity(idz);
                ref var data = ref caviarEntity.Get<TadpoleDataComponent>().Value;

                for (int i = 0; i < _data.SaveData.TadpoleByJar.Length; i++)
                {
                    if (_data.SaveData.TadpoleByJar[i] == -1)
                    {
                        _data.SaveData.TadpoleSaveData.Add(new TadpoleSaveData()
                        {
                            TadpoleType = data.TadpoleType,
                            TadpoleName = Names.GetRandom(),
                            Ingredients = new Dictionary<IngredientType, int>(),
                            IngredientsToday = new Dictionary<IngredientType, int>(),
                            FatAmount = 1,
                            IsDead = false,
                            IsFed = false,
                            MetamorphosisStep = 0
                        });

                        _audioService.Play(Sounds.PopSound);
        
                        _data.SaveData.TadpoleByJar[i] = _data.SaveData.TadpoleSaveData.Count - 1;
                        break;
                        //_data.RuntimeData.CurrentTadpole = _data.SaveData.TadpoleByJar[i];
                    }
                }
                
                _prefabFactory.Despawn(ref caviarEntity);
            }
        }
    }
}
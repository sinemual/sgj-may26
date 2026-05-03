using Assets.Scripts.ECS._Features.Stats;
using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class SleepSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private UserInterface _ui;

        private EcsFilter<SleepRequest> _filter;
        private EcsFilter<TadpoleProvider>.Exclude<DeadState> _tadpoleFilter;

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
                        if (data.IsDead)
                        {
                            Debug.Log($"data.IsDead {data.IsDead}");
                            foreach (var idz in _tadpoleFilter)
                            {
                                ref var tadpoleEntity = ref _tadpoleFilter.GetEntity(idz);
                                if (tadpoleEntity.Get<SaveId>().Value == _data.SaveData.TadpoleByJar[i])
                                {
                                    tadpoleEntity.Get<DeadState>();
                                    tadpoleEntity.Get<TadpoleProvider>().CaviarMetamorphosisStepView.SetActive(false);
                                    tadpoleEntity.Get<TadpoleProvider>().TadpoleMetamorphosisStepView.SetActive(false);
                                    _ui.GetScreen<HomeScreen>().UpdateNumberText(" ");
                                    _ui.GetScreen<HomeScreen>().UpdateTadpoleNameText("Empty");
                                    _data.SaveData.TadpoleByJar[i] = -1;
                                }
                            }
                        }

                        if (!data.IsFed)
                        {
                            data.Health -= 1;
                        }
                        else
                        {
                            data.MetamorphosisStep += 1;
                            data.IsFed = false;
                        }

                        if (data.Health < 0.0f)
                            data.IsDead = true;
                    }
                }

                foreach (var idz in _tadpoleFilter)
                {
                    ref var tadpoleEntity = ref _tadpoleFilter.GetEntity(idz);
                    ref var stats = ref tadpoleEntity.Get<Stats>();
                    //Debug.Log($"{tadpoleEntity.Get<GameObjectProvider>().Value}", tadpoleEntity.Get<GameObjectProvider>().Value);
                    tadpoleEntity.Get<UpdateTadpoleViewRequest>();

                    if (!_data.SaveData.TadpoleSaveData[tadpoleEntity.Get<SaveId>().Value].IsDead)
                        _data.RuntimeData.UpdateStatsByIngredients(ref stats, ref tadpoleEntity.Get<SaveId>().Value);
                }

                _data.RuntimeData.IsTodayGathered = false;
                _ui.GetScreen<GameScreen>().UpdateDayText(_data.SaveData.Day);


                entity.Del<SleepRequest>();
            }
        }
    }

    public struct UpdateTadpoleViewRequest : IEcsIgnoreInFilter
    {
    }
}
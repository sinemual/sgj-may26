using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class HomeSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private UserInterface _ui;

        private EcsFilter<HomeProvider>.Exclude<InitedMarker> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var spawnPoints = ref entity.Get<HomeProvider>().SpawnPoints;
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;

                _ui.GetScreen<GameScreen>().ShowTextPanel(_data.StaticData.TextData.Texts[TextType.HomeScreen]);
                
                for (int i = 0; i < _data.SaveData.TadpoleByJar.Length; i++)
                {
                    if (_data.SaveData.TadpoleByJar[i] != -1)
                    {
                        var saveData = _data.SaveData.TadpoleSaveData[_data.SaveData.TadpoleByJar[i]];
                        var data = _data.StaticData.TadpoleDataByType[saveData.TadpoleType];
                        
                        EcsEntity playerEntity = _prefabFactory.Spawn(data.Prefab, spawnPoints[i].position, spawnPoints[i].rotation, entityGo.transform);
                        playerEntity.Get<TadpoleDataComponent>().Value = data;
                        playerEntity.Get<SaveId>().Value = _data.SaveData.TadpoleByJar[i];
                        playerEntity.Get<RigidbodyProvider>().Value.isKinematic = true;
                        playerEntity.Get<UpdateTadpoleViewRequest>();
                        playerEntity.Get<TadpoleProvider>().Collider.enabled = false;
                        playerEntity.Get<AnimatorProvider>().Value.SetTrigger(Animations.IsInJar);
                        if (saveData.IsDead)
                        {
                            playerEntity.Get<DeadState>();
                            playerEntity.Get<TadpoleProvider>().CaviarMetamorphosisStepView.SetActive(false);
                            playerEntity.Get<TadpoleProvider>().TadpoleMetamorphosisStepView.SetActive(false);
                            _ui.GetScreen<HomeScreen>().UpdateNumberText(" ");
                            _ui.GetScreen<HomeScreen>().UpdateTadpoleNameText("Empty");
                            _data.SaveData.TadpoleByJar[i] = -1;
                        }
                    }
                }
                
                entity.Get<InitedMarker>();
            }
        }
    }
}
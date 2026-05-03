using Client.Data;
using Client.Data.Core;
using Client.Data.Equip;
using Client.ECS.CurrentGame.Mining;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class HomeScreenSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SharedData _data;
        private UserInterface _ui;
        private AudioService _audioService;
        private CameraService _cameraService;

        private EcsFilter<HeroProvider> _playerFilter;

        public void Init()
        {
            _ui.GetScreen<HomeScreen>().ShowScreen += () =>
            {
                if (_data.SaveData.TadpoleByJar[_data.RuntimeData.CurrentHomePoint] == -1)
                {
                    EmptyInfo();
                }
                else
                {
                    _data.RuntimeData.CurrentTadpole = _data.SaveData.TadpoleByJar[_data.RuntimeData.CurrentHomePoint];
                    UpdateTadpoleInfo();
                }

                _ui.GetScreen<HomeScreen>().UpdateIngredients(_data.SaveData.Ingredients, _data.StaticData.ItemData);
            };

            _ui.GetScreen<HomeScreen>().FeedButtonClick += () =>
            {
                _world.NewEntity().Get<FeedRequest>();
                _audioService.Play(Sounds.UiClickSound);
            };

            _ui.GetScreen<HomeScreen>().AddIngredientButtonClick += (ingredient) =>
            {
                Debug.Log($"ingredient {ingredient}");
                _world.NewEntity().Get<AddIngredientRequest>().Value = ingredient;
                _audioService.Play(Sounds.UiClickSound);
            };

            _ui.GetScreen<HomeScreen>().FlushButtonClick += () =>
            {
                _world.NewEntity().Get<FlushRequest>();
                _audioService.Play(Sounds.UiClickSound);
            };

            _ui.GetScreen<HomeScreen>().SleepButtonClick += () =>
            {
                EcsEntity sleepRequestEntity = _world.NewEntity();
                sleepRequestEntity.Get<Timer<DelayTimer>>().Value = 1.0f;
                sleepRequestEntity.Get<SleepRequest>();
                _ui.GetScreen<GameScreen>().Sleep();
                _audioService.Play(Sounds.UiClickSound);
            };

            _ui.GetScreen<HomeScreen>().NextButtonClick += () =>
            {
                _world.NewEntity().Get<ChangeLookJarRequest>().IsNext = true;
                _audioService.Play(Sounds.UiClickSound);
            };

            _ui.GetScreen<HomeScreen>().PreviousButtonButtonClick += () =>
            {
                _world.NewEntity().Get<ChangeLookJarRequest>().IsNext = false;
                _audioService.Play(Sounds.UiClickSound);
            };
            
              
            _ui.GetScreen<HomeScreen>().GoToCatchingButtonClick += () =>
            {
                _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.CatchingStep;
                EcsEntity goToRequestEntity = _world.NewEntity();
                goToRequestEntity.Get<GoToCatchingRequest>();
                goToRequestEntity.Get<DespawnLevelRequest>();
                _audioService.Play(Sounds.UiClickSound);
            };
            
            _ui.GetScreen<HomeScreen>().GoToGatheringButtonClick += () =>
            {
                _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.GatheringStep;
                EcsEntity goToRequestEntity = _world.NewEntity();
                goToRequestEntity.Get<GoToGatheringRequest>();
                goToRequestEntity.Get<DespawnLevelRequest>();
                _audioService.Play(Sounds.UiClickSound);
            };
            
                        
            _ui.GetScreen<HomeScreen>().StartRaceButtonClick += () =>
            {
                if (_data.RuntimeData.CurrentTadpole == -1 || _data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].MetamorphosisStep == 0)
                {
                    _audioService.Play(Sounds.UiClickSound);;
                    return;
                }
                
                _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.RaceStep;
                EcsEntity goToRequestEntity = _world.NewEntity();
                goToRequestEntity.Get<StartRaceRequest>();
                goToRequestEntity.Get<DespawnLevelRequest>();
                _audioService.Play(Sounds.UiClickSound);
            };
        }

        private void UpdateTadpoleInfo()
        {
            _ui.GetScreen<HomeScreen>().UpdateTadpoleNameText(_data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].TadpoleName);
            _ui.GetScreen<HomeScreen>().UpdateNumberText(_data.RuntimeData.CurrentTadpole.ToString());
        }

        private void EmptyInfo()
        {
            _ui.GetScreen<HomeScreen>().UpdateTadpoleNameText("Empty");
            _ui.GetScreen<HomeScreen>().UpdateNumberText(" ");
        }
    }

    public struct AddIngredientRequest
    {
        public ItemData Value;
    }

    public struct ChangeLookJarRequest
    {
        public bool IsNext;
    }

    public struct FeedRequest
    {
    }

    public struct FlushRequest
    {
    }

    public struct SleepRequest
    {
    }
}
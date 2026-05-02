using Client.Data;
using Client.Data.Core;
using Client.Data.Equip;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

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

            };
            
            _ui.GetScreen<HomeScreen>().FeedButtonClick += () =>
            {
                _world.NewEntity().Get<FeedRequest>();
                _audioService.Play(Sounds.UiClickSound);
            };
            
            _ui.GetScreen<HomeScreen>().AddIngredientButtonClick += (ingredient) =>
            {
                _world.NewEntity().Get<AddIngredientRequest>().Value = ingredient;
                _ui.GetScreen<HomeScreen>().UpdateIngredients(_data.SaveData.Ingredients, _data.StaticData.ItemData);
                _audioService.Play(Sounds.UiClickSound);
            };
            
            _ui.GetScreen<HomeScreen>().FlushButtonClick += () =>
            {
                _world.NewEntity().Get<FlushRequest>();
                _audioService.Play(Sounds.UiClickSound);
            };
            
            _ui.GetScreen<HomeScreen>().SleepButtonClick += () =>
            {
                _world.NewEntity().Get<SleepRequest>();
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
        }

        private void UpdateTadpoleInfo()
        {
            _ui.GetScreen<HomeScreen>().UpdateTadpoleNameText(_data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].TadpoleName);
            _ui.GetScreen<HomeScreen>().UpdateNumberText(_data.RuntimeData.CurrentTadpole.ToString());
            _ui.GetScreen<HomeScreen>().UpdateIngredients(_data.SaveData.Ingredients, _data.StaticData.ItemData);
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
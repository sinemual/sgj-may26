using System.Collections.Generic;
using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class GameScreenSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SharedData _data;
        private UserInterface _ui;
        private AudioService _audioService;
        
        private EcsFilter<HeroProvider> _playerFilter;

        public void Init()
        {
            _ui.GetScreen<GameScreen>().ShowScreen += () =>
            {
                _ui.GetScreen<GameScreen>().UpdateDayText(_data.SaveData.Day);
            };
                
            _ui.GetScreen<GameScreen>().PauseButtonClick += () =>
            {
                Time.timeScale = 0.0f;
                _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.Pause;
                _ui.ShowScreen<SettingsScreen>();
                _ui.GetScreen<SettingsScreen>().UpdateView();
                _audioService.Play(Sounds.UiClickSound);
            };
            
            _ui.GetScreen<GameScreen>().GoToCatchingButtonClick += () =>
            {
                _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.CatchingStep;
                EcsEntity goToRequestEntity = _world.NewEntity();
                goToRequestEntity.Get<GoToCatchingRequest>();
                goToRequestEntity.Get<DespawnLevelRequest>();
                _audioService.Play(Sounds.UiClickSound);
            };
            
            _ui.GetScreen<GameScreen>().GoToGatheringButtonClick += () =>
            {
                _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.GatheringStep;
                EcsEntity goToRequestEntity = _world.NewEntity();
                goToRequestEntity.Get<GoToGatheringRequest>();
                goToRequestEntity.Get<DespawnLevelRequest>();
                _audioService.Play(Sounds.UiClickSound);
            };
            
            _ui.GetScreen<GameScreen>().GoToHomeButtonClick += () =>
            {
                _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.HomeStep;
                EcsEntity goToRequestEntity = _world.NewEntity();
                goToRequestEntity.Get<GoToHomeRequest>();
                goToRequestEntity.Get<DespawnLevelRequest>();
                _audioService.Play(Sounds.UiClickSound);
            };
            
            _ui.GetScreen<GameScreen>().StartRaceButtonClick += () =>
            {
                _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.RaceStep;
                EcsEntity goToRequestEntity = _world.NewEntity();
                goToRequestEntity.Get<StartRaceRequest>();
                goToRequestEntity.Get<DespawnLevelRequest>();
                _audioService.Play(Sounds.UiClickSound);
            };
        }
    }

    public struct StartRaceRequest : IEcsIgnoreInFilter
    {
    }
    
    public struct GoToGatheringRequest : IEcsIgnoreInFilter
    {
    }
    
    public struct GoToCatchingRequest : IEcsIgnoreInFilter
    {
    }
    
    public struct GoToHomeRequest : IEcsIgnoreInFilter
    {
    }
}
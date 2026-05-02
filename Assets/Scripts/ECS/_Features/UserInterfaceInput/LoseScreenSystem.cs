using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class LoseScreenSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SharedData _data;
        private UserInterface _ui;
        private AudioService _audioService;
        
        private EcsFilter<HeroProvider> _playerFilter;

        public void Init()
        {
            _ui.GetScreen<LoseScreen>().ShowScreen += () =>
            {
                _ui.GetScreen<LoseScreen>().UpdatePlaceInRaceText(_data.RuntimeData.PlaceInRace, _data.RuntimeData.FinishersCounter);
            };
            
            _ui.GetScreen<LoseScreen>().GoToHomeButtonClick += () =>
            {
                _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.HomeStep;
                _ui.HideScreen<LoseScreen>();
                _world.NewEntity().Get<FlushRequest>();
                _audioService.Play(Sounds.UiClickSound);
            };
        }
    }
}
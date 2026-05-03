using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class WinScreenSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SharedData _data;
        private UserInterface _ui;
        private AudioService _audioService;

        public void Init()
        {
            _ui.GetScreen<WinScreen>().ShowScreen += () =>
            {
                if ((int)_data.RuntimeData.RaceStep == 2)
                {
                    _ui.GetScreen<WinScreen>().GoToHomeButton.gameObject.SetActive(true);
                    _ui.GetScreen<WinScreen>().NextStepButton.gameObject.SetActive(false);
                }
                else
                {
                    _ui.GetScreen<WinScreen>().GoToHomeButton.gameObject.SetActive(false);
                    _ui.GetScreen<WinScreen>().NextStepButton.gameObject.SetActive(true);
                }
            };

            _ui.GetScreen<WinScreen>().GoToHomeButtonClick += () =>
            {
                _ui.HideScreen<WinScreen>();
                _audioService.Play(Sounds.UiClickSound);

                if ((int)_data.RuntimeData.RaceStep == 2)
                {
                    _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.GameEnd;
                    _ui.ShowScreen<OutroScreen>();
                }
                else
                {
                    _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.HomeStep;
                }
            };

            _ui.GetScreen<WinScreen>().NextStepButtonClick += () =>
            {
                _data.RuntimeData.RaceStep += 1;
                EcsEntity goToRequestEntity = _world.NewEntity();
                goToRequestEntity.Get<StartRaceRequest>();
                goToRequestEntity.Get<DespawnLevelRequest>();
                _ui.HideScreen<WinScreen>();
                _audioService.Play(Sounds.UiClickSound);
            };
        }
    }
}

using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

namespace Client
{
    public class UserInterfaceByGameStateSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private UserInterface _ui;
        
        private EcsFilter<GameStateChangedEvent> _eventFilter;

        public void Run()
        {
            foreach (var idx in _eventFilter)
            {
                ref var entity = ref _eventFilter.GetEntity(idx);

                _ui.GetScreen<GameScreen>().GoToCatchingButton.gameObject.SetActive(false);
                _ui.GetScreen<GameScreen>().GoToGatheringButton.gameObject.SetActive(false);
                _ui.GetScreen<GameScreen>().StartRaceButton.gameObject.SetActive(false);
                _ui.GetScreen<GameScreen>().GoToHomeButton.gameObject.SetActive(false);
                
                if (_data.RuntimeData.CurrentGameStateType == GameStateType.CatchingStep)
                {
                    _ui.GetScreen<GameScreen>().GoToHomeButton.gameObject.SetActive(true);
                    _ui.HideScreen<HomeScreen>();
                }
                
                if (_data.RuntimeData.CurrentGameStateType == GameStateType.GatheringStep)
                {
                    _ui.GetScreen<GameScreen>().GoToHomeButton.gameObject.SetActive(true);
                    _ui.HideScreen<HomeScreen>();
                }
                
                if (_data.RuntimeData.CurrentGameStateType == GameStateType.HomeStep)
                {
                    _ui.GetScreen<GameScreen>().GoToCatchingButton.gameObject.SetActive(true);
                    _ui.GetScreen<GameScreen>().GoToGatheringButton.gameObject.SetActive(true);
                    _ui.GetScreen<GameScreen>().StartRaceButton.gameObject.SetActive(true);
                    _ui.ShowScreen<HomeScreen>();
                }
                
                if (_data.RuntimeData.CurrentGameStateType == GameStateType.RaceStep)
                {
                    _ui.HideScreen<HomeScreen>();
                }
            }
        }
    }
}
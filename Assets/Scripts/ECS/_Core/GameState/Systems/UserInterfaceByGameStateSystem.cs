using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Data;
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
                _ui.GetScreen<GameScreen>().HideTextPanel();
                _ui.GetScreen<HomeScreen>().GoToCatchingButton.gameObject.SetActive(false);
                _ui.GetScreen<HomeScreen>().GoToGatheringButton.gameObject.SetActive(false);
                _ui.GetScreen<HomeScreen>().StartRaceButton.gameObject.SetActive(false);
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
                    _ui.GetScreen<HomeScreen>().GoToCatchingButton.gameObject.SetActive(true);
                    _ui.GetScreen<HomeScreen>().GoToGatheringButton.gameObject.SetActive(true);
                    _ui.GetScreen<HomeScreen>().StartRaceButton.gameObject.SetActive(true);
                    _ui.ShowScreen<HomeScreen>();
                }
                
                if (_data.RuntimeData.CurrentGameStateType == GameStateType.RaceStep)
                {
                    _ui.HideScreen<HomeScreen>();
                }
                
                
                if (_data.RuntimeData.CurrentGameStateType == GameStateType.GameEnd)
                {
                    _ui.ShowScreen<OutroScreen>();
                    _ui.GetScreen<GameScreen>().ShowTextPanel(_data.StaticData.TextData.Texts[TextType.Outro]);
                    _ui.ReorderScreens();
                }
            }
        }
    }
}
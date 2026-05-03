using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

namespace Client
{
    public class IntroAndOutroScreenSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SharedData _data;
        private UserInterface _ui;
        private AudioService _audioService;

        public void Init()
        {
            _ui.GetScreen<IntroScreen>().SkipButtonClick += () =>
            {
                _ui.HideScreen<IntroScreen>();
            };

            _ui.GetScreen<OutroScreen>().SkipButtonClick += () =>
            {
                _ui.HideScreen<OutroScreen>();
            };
        }
    }
}
using System;
using System.Collections.Generic;
using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Data;
using Leopotam.Ecs;
using UnityEngine;
using CameraType = Client.Data.CameraType;

namespace Client
{
    public class InitGameSystem : IEcsInitSystem
    {
        private SharedData _data;
        private UserInterface _ui;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private AudioService _audioService;
        private TimeManagerService _timeManagerService;
        private CameraService _cameraService;
        
        public void Init()
        {
            /*if (_data.PlayerData.IsGameLaunchedBefore)
                _ui.MainMenuScreen.SetShowState(true);
            else
                _world.NewEntity().Get<SpawnLevelRequest>();*/
            //_world.NewEntity().Get<SpawnLevelRequest>();

            //_world.NewEntity().Get<SpawnMenuLevelRequest>();
            _data.RuntimeData.CurrentTadpole = -1;
            
            _audioService.Play(Sounds.MusicGameplaySound);
            _ui.ShowScreen<GameScreen>();
            _ui.ShowScreen<HomeScreen>();
            _ui.ShowScreen<IntroScreen>();
            _ui.ReorderScreens();
            
            //_ui.ShowScreen<SettingsScreen>();
            //_cameraService.SetCamera(CameraType.None);

            //debug
            /*if (_data.SaveData.TadpoleSaveData.Count == 0)
            {
                var ingredients = new Dictionary<IngredientType, int>();
                ingredients.Add(IngredientType.Dung, 1);
                _data.RuntimeData.CurrentTadpole = 0;
                _data.SaveData.TadpoleByJar[0] = 0;
                _data.SaveData.TadpoleSaveData.Add(new TadpoleSaveData()
                {
                    TadpoleType = TadpoleType.None,
                    TadpoleName = "Debugy",
                    Ingredients = ingredients
                });
            }*/

            _world.NewEntity().Get<GoToHomeRequest>();            
            _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.HomeStep;            

            //_world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.Init;
        }
    }
}
using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

namespace Client
{
    public class ChangeJarSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private CameraService _cameraService;
        private UserInterface _ui;

        private EcsFilter<ChangeLookJarRequest> _filter;
        private EcsFilter<HomeProvider> _homeFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var isNext = ref entity.Get<ChangeLookJarRequest>().IsNext;

                var cameraPoints = _homeFilter.Get1(0).CameraPoints;
                
                if (isNext)
                {
                    _data.RuntimeData.CurrentHomePoint += 1;
                    if (_data.RuntimeData.CurrentHomePoint > cameraPoints.Length - 1)
                        _data.RuntimeData.CurrentHomePoint = 0;
                }
                else
                {
                    _data.RuntimeData.CurrentHomePoint -= 1;
                    if (_data.RuntimeData.CurrentHomePoint < 0)
                        _data.RuntimeData.CurrentHomePoint = cameraPoints.Length - 1;
                }
                
                _cameraService.SetCamera(CameraType.Home, cameraPoints[_data.RuntimeData.CurrentHomePoint],
                    cameraPoints[_data.RuntimeData.CurrentHomePoint]);
                _data.RuntimeData.CurrentTadpole = _data.SaveData.TadpoleByJar[_data.RuntimeData.CurrentHomePoint];

                if (_data.SaveData.TadpoleByJar[_data.RuntimeData.CurrentHomePoint] == -1)
                {
                    EmptyInfo();
                }
                else
                {
                    UpdateTadpoleInfo();
                }
               
                
                entity.Del<ChangeLookJarRequest>();
            }
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
}
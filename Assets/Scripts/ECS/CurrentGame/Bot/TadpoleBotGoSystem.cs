using Client.Data.Core;
using Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TadpoleBotGoSystem : IEcsRunSystem
    {
        private SharedData _data;
        private CameraService _cameraService;
        private AudioService _audioService;

        private EcsFilter<TadpoleProvider>.Exclude<Timer<ThinkingTimer>, PlayerTag> _filter;
        private EcsFilter<RaceManagerProvider, InitedMarker> _raceFilter;

        public void Run()
        {
            foreach (var idz in _raceFilter)
            {
                ref var entityRace = ref _raceFilter.GetEntity(idz);
                ref var botPath = ref entityRace.Get<RaceManagerProvider>().BotPath;
                
                foreach (var idx in _filter)
                {
                    ref var entity = ref _filter.GetEntity(idx);
                    ref var entityGo = ref entity.Get<GameObjectProvider>().Value;
                    ref var animator = ref entity.Get<AnimatorProvider>().Value;
                    ref var entityRb = ref entity.Get<RigidbodyProvider>().Value;
                    ref var currentPoint = ref entity.Get<CurrentPoint>().Value;

                    if (currentPoint >= botPath.Length)
                        return;
                    //Debug.Log($"currentPoint {currentPoint}");

                    if (Vector3.Distance(botPath[currentPoint].position, entityGo.transform.position) <
                        _data.BalanceData.BotCheckPointRadius)
                        currentPoint += 1;

                    if (currentPoint >= botPath.Length)
                        return;
                    
                    Vector3 random = Random.onUnitSphere * _data.BalanceData.BotRandomTargetRadius;
                    random = random.SetY(0.0f);
                    Vector3 target = botPath[currentPoint].position + random;

                    entity.Get<LureRequest>().Position = target;

                    _data.SceneData.BotTarget.position = target;
                }
            }
        }
    }
}
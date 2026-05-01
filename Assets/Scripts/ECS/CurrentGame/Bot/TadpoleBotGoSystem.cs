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

        private EcsFilter<TadpoleProvider>.Exclude<Timer<ThinkingTimer>, PlayerTagProvider> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;
                ref var animator = ref entity.Get<AnimatorProvider>().Value;
                ref var entityRb = ref entity.Get<RigidbodyProvider>().Value;
                ref var currentPoint = ref entity.Get<CurrentPoint>().Value;

                if(currentPoint >= _data.SceneData.BotPath.Length - 1)
                    return;
                
                if (Vector3.Distance(_data.SceneData.BotPath[currentPoint].position, entityGo.transform.position) <
                    _data.BalanceData.BotCheckPointRadius)
                    currentPoint += 1;

                Vector3 random = Random.onUnitSphere * _data.BalanceData.BotRandomTargetRadius;
                random = random.SetY(0.0f);
                Vector3 target = _data.SceneData.BotPath[currentPoint].position + random;

                entity.Get<LureRequest>().Position = target;
                
                _data.SceneData.BotTarget.position = target;
            }
        }
    }
}
using Client.Data;
using Client.Data.Core;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class FinishTriggerSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private AudioService _audioService;

        private EcsFilter<FinishTagProvider, OnTriggerEnterEvent> _enterFilter;
        private EcsFilter<FinishTagProvider, OnTriggerExitEvent> _exitFilter;

        public void Run()
        {
            foreach (var idx in _enterFilter)
            {
                ref var entity = ref _enterFilter.GetEntity(idx);
                ref var entityCollision = ref entity.Get<OnTriggerEnterEvent>();
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;

                if (entityCollision.Collider && entityCollision.Collider.TryGetComponent(out MonoEntity monoEntity))
                {
                    if (monoEntity.Entity.IsAlive() && monoEntity.Entity.Has<TadpoleProvider>() && !monoEntity.Entity.Has<FinisherMarker>())
                    {
                        monoEntity.Entity.Get<FinisherMarker>();
                        _data.RuntimeData.FinishersCounter += 1;
                        if (!_data.RuntimeData.IsCurrentRaceFinishedForPlayer && monoEntity.Entity.Has<PlayerTag>())
                        {
                            _data.RuntimeData.PlaceInRace = _data.RuntimeData.FinishersCounter;
                            _data.RuntimeData.IsCurrentRaceFinishedForPlayer = true;

                            if (_data.RuntimeData.PlaceInRace != 1)
                            {
                                _audioService.Play(Sounds.WinSound);
                                monoEntity.Entity.Get<AnimatorProvider>().Value.SetTrigger(Animations.IsWin);
                                _world.NewEntity().Get<LoseEvent>();
                            }
                            else
                            {
                                _audioService.Play(Sounds.LoseSound);
                                monoEntity.Entity.Get<AnimatorProvider>().Value.SetTrigger(Animations.IsLose);
                                _world.NewEntity().Get<WinEvent>();
                            }
                        }
                    }
                }
            }

            /*foreach (var idx in _exitFilter)
            {
                ref var entity = ref _exitFilter.GetEntity(idx);
                ref var entityCollision = ref entity.Get<OnTriggerExitEvent>();

                if (entityCollision.Collider.gameObject.CompareTag(_data.StaticData.GroundTag))
                {
                }
            }*/
        }
    }

    public struct FinisherMarker
    {
    }
}
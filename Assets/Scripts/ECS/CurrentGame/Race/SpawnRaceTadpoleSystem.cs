using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class SpawnRaceTadpoleSystem : IEcsRunSystem
    {
        private SharedData _data;
        private CameraService _cameraService;
        private AudioService _audioService;
        private PrefabFactory _prefabFactory;

        private EcsFilter<RaceManagerProvider>.Exclude<InitedMarker> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var spawnPoints = ref entity.Get<RaceManagerProvider>().SpawnPoints;

                _data.RuntimeData.IsCurrentRaceFinishedForPlayer = false;
                _data.RuntimeData.PlaceInRace = 10;
                _data.RuntimeData.FinishersCounter = 0;

                var raceParticipantsAmountByStep = _data.BalanceData.RaceParticipantsAmountByStep[_data.RuntimeData.RaceStep];
                var playerPointNum = Random.Range(0, raceParticipantsAmountByStep);

                for (int i = 0; i < raceParticipantsAmountByStep; i++)
                {
                    if (i != playerPointNum)
                    {
                        var botData = _data.StaticData.TadpoleData[Random.Range(0, _data.StaticData.TadpoleData.Count)];
                        EcsEntity botEntity = _prefabFactory.Spawn(botData.Prefab,
                            spawnPoints[i].position, Quaternion.identity);
                        botEntity.Get<TadpoleDataComponent>().Value = botData;
                        botEntity.Get<UpdateTadpoleViewRequest>();
                    }
                    else
                    {
                        var playerData =
                            _data.StaticData.TadpoleDataByType[_data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].TadpoleType];
                        EcsEntity playerEntity = _prefabFactory.Spawn(playerData.Prefab, spawnPoints[i].position, Quaternion.identity);
                        playerEntity.Get<TadpoleDataComponent>().Value = playerData;
                        playerEntity.Get<SaveId>().Value = _data.RuntimeData.CurrentTadpole;
                        playerEntity.Get<PlayerTag>();
                        playerEntity.Get<UpdateTadpoleViewRequest>();
                    }
                }

                entity.Get<InitedMarker>();
            }
        }
    }
}
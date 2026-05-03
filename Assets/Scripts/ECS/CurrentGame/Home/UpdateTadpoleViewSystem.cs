using Assets.Scripts.ECS._Features.Stats;
using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using Unity.Mathematics.Geometry;
using UnityEngine;

namespace Client
{
    public class UpdateTadpoleViewSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private UserInterface _ui;

        private EcsFilter<UpdateTadpoleViewRequest, InitedMarker> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var tadpole = ref entity.Get<TadpoleProvider>();
                ref var tadpoleGo = ref entity.Get<GameObjectProvider>().Value;
                ref var animator = ref entity.Get<AnimatorProvider>().Value;
                ref var saveId = ref entity.Get<SaveId>().Value;
                ref var stats = ref entity.Get<Stats>().Value;

                Debug.Log($"UpdateTadpoleViewRequest");
                var saveData = _data.SaveData.TadpoleSaveData[saveId];
                var data = _data.StaticData.TadpoleDataByType[saveData.TadpoleType];

                tadpole.CaviarMetamorphosisStepView.SetActive(saveData.MetamorphosisStep == 0);
                tadpole.TadpoleMetamorphosisStepView.SetActive(saveData.MetamorphosisStep > 0);

                for (int i = 0; i < tadpole.TadpoleMeshRenderers.Length; i++)
                    tadpole.TadpoleMeshRenderers[i].material.color = _data.StaticData.ColorByValue[stats[StatType.Color].GetValue()];
                tadpole.CaviarMeshRenderer.material.color = _data.StaticData.ColorByValue[stats[StatType.Color].GetValue()];

                if (entity.Has<PlayerTag>())
                {
                    Debug.Log($"stats[StatType.Color].GetValue(): {stats[StatType.Color].GetValue()}");
                    Debug.Log($"stats[StatType.Fat].GetValue(): {stats[StatType.Fat].GetValue()}");
                }

                tadpoleGo.transform.localScale = Vector3.one * stats[StatType.Fat].GetValue();

                if (saveData.IsDead)
                    animator.SetTrigger(Animations.IsDie);

                if (!entity.Has<PlayerTag>())
                {
                    tadpole.CaviarMetamorphosisStepView.SetActive(false);
                    tadpole.TadpoleMetamorphosisStepView.SetActive(true);

                    for (int i = 0; i < tadpole.TadpoleMeshRenderers.Length; i++)
                        tadpole.TadpoleMeshRenderers[i].material.color = _data.StaticData.BotColor;
                }

                entity.Del<UpdateTadpoleViewRequest>();
            }
        }
    }
}
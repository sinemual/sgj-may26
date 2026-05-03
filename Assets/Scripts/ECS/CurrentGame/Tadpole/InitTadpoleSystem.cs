using System.Collections.Generic;
using Assets.Scripts.ECS._Features.Stats;
using Client.Data.Core;
using Client.DevTools.MyTools;
using Client.ECS.CurrentGame.Player;
using Data;
using Extensions;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class InitTadpoleSystem : IEcsRunSystem
    {
        private SharedData _data;
        private CameraService _cameraService;
        private AudioService _audioService;

        private EcsFilter<TadpoleProvider>.Exclude<InitedMarker> _filter;
        private EcsFilter<RaceManagerProvider, InitedMarker> _raceFilter;
        private EcsFilter<HomeProvider, InitedMarker> _homeFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;
                ref var animator = ref entity.Get<AnimatorProvider>().Value;
                ref var entityRb = ref entity.Get<RigidbodyProvider>().Value;
                ref var data = ref entity.Get<TadpoleDataComponent>().Value;
                ref var saveId = ref entity.Get<SaveId>().Value;

                ref var stats = ref entity.Get<Stats>();
                stats.Value = new Dictionary<StatType, Stat>();

                foreach (var stat in _data.StaticData.TadpoleDataByType[data.TadpoleType].Stats)
                {
                    var copiedStat = new Stat(stat.Value.GetBaseValue());
                    stats.Value.Add(stat.Key, copiedStat);
                }

                if (!_homeFilter.IsEmpty())
                    entity.Get<PlayerTag>();

                if (entity.Has<PlayerTag>())
                    _data.RuntimeData.UpdateStatsByIngredients(ref stats, ref saveId);
                else
                    GenerateTadpoleBot(ref entity, ref stats);

                /*if (!_raceFilter.IsEmpty() && entity.Has<PlayerTag>())
                    entity.Get<TadpoleProvider>().Arrow.SetActive(true);*/

                entity.Get<CurrentPoint>().Value = 0;
                entity.Get<Health>().Value = stats.Value[StatType.Health].GetValue();
                entity.Get<InitedMarker>();
                entity.Get<UpdateTadpoleViewRequest>();
            }
        }


        private void GenerateTadpoleBot(ref EcsEntity entity, ref Stats stats)
        {
            var ingredients = new Dictionary<IngredientType, int>(); //generate random stats
            int maxIngredients = ((int)_data.RuntimeData.RaceStep + 1) * 2;
            ingredients.Add(IngredientType.Dung, Random.Range(0, maxIngredients));
            ingredients.Add(IngredientType.Berry, Random.Range(0, maxIngredients));
            ingredients.Add(IngredientType.Mushroom, Random.Range(0, maxIngredients));
            ingredients.Add(IngredientType.Lavender, Random.Range(0, maxIngredients));
            ingredients.Add(IngredientType.PineCone, Random.Range(0, maxIngredients));

            entity.Get<BotTadpoleSaveData>().Value = new TadpoleSaveData()
            {
                TadpoleType = TadpoleType.None,
                TadpoleName = Names.GetRandom(),
                Ingredients = ingredients
            };

            foreach (var stat in stats.Value)
                stat.Value.RemoveAllModifiers();

            foreach (var ingredient in ingredients)
            {
                var ingredientData = _data.StaticData.IngredientDataByType[ingredient.Key];
                var statModifier = new StatModifier()
                {
                    Type = ingredientData.StatModifierType,
                    Value = ingredientData.GetStatModifierValueByLevel(ingredient.Value)
                };
                stats.Value[ingredientData.StatType].AddModifier(statModifier);
                stats.Value[ingredientData.StatType].SaveThisUpgradeModifier();
            }

            stats.Value[StatType.Fat].SetAllValues(Random.Range(1.0f, 2.0f));
        }
    }
}

public enum TadpoleType
{
    None = 0,
    Fast = 1,
    Smart = 2,
}

public struct BotTadpoleSaveData
{
    public TadpoleSaveData Value;
}

public struct SaveId
{
    public int Value;
}
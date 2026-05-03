using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class AddIngredientSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private UserInterface _ui;

        private EcsFilter<AddIngredientRequest> _filter;
        private EcsFilter<TadpoleProvider> _tadpoleFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var ingredient = ref entity.Get<AddIngredientRequest>().Value;

                if (_data.RuntimeData.CurrentTadpole == -1)
                {
                    entity.Del<AddIngredientRequest>();
                    continue;
                }
                
                _data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].Ingredients.TryAdd(ingredient.IngredientType, 0);
                _data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].IngredientsToday.TryAdd(ingredient.IngredientType, 0);
                    
                _data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].Ingredients[ingredient.IngredientType] += 1;
                _data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].IngredientsToday[ingredient.IngredientType] += 1;
                
                _data.SaveData.Ingredients[ingredient.IngredientType] -= 1;
                _ui.GetScreen<HomeScreen>().UpdateIngredients(_data.SaveData.Ingredients, _data.StaticData.ItemData);

                _prefabFactory.Spawn(_data.StaticData.IngredientDataByType[ingredient.IngredientType].ItemView.DropItemPrefab,
                    _data.SceneData.SpawnFoodPoint.position, Quaternion.identity);

                entity.Del<AddIngredientRequest>();
            }
        }
    }
}
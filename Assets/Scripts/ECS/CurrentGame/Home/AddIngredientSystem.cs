using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;

namespace Client
{
    public class AddIngredientSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<AddIngredientRequest> _filter;
        private EcsFilter<TadpoleProvider> _tadpoleFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var ingredient = ref entity.Get<AddIngredientRequest>().Value;
                
                _data.SaveData.TadpoleSaveData[_data.RuntimeData.CurrentTadpole].Ingredients[ingredient.IngredientType] += 1;

                entity.Del<AddIngredientRequest>();
            }
        }
    }
}
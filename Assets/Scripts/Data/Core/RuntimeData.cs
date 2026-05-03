using System;
using Assets.Scripts.ECS._Features.Stats;
using Client.Data;
using Client.Data.Core;
using Client.ECS.CurrentGame.Player;
using Data.Base;
using Leopotam.Ecs;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class RuntimeData : BaseData
    {
        public GameStateType CurrentGameStateType;
        public GameStateType PreviousGameStateType;

        public int CurrentTaskId = -1;
        public float LevelProgress;

        public int LastLevelIdx = -1;
        
        public bool IsWeHaveInternetTime;
        public bool IsLoopedLevel;
        
        
        public bool IsCurrentRaceFinishedForPlayer;
        public RaceStep RaceStep;
        public int PlaceInRace;
        public int FinishersCounter;
        public int CurrentTadpole;
        public int CurrentHomePoint;
        public bool IsTodayGathered;
        
        public void InjectData(SharedData sharedData) => base.SharedData = sharedData;

        public override void ResetData()
        {
        }

        public void ResetLevelData()
        {
            LevelProgress = 0;
        }

        public int GetLevelReward()
        {
            int value = SharedData.SaveData.EventLevelIdx * SharedData.BalanceData.BaseLevelReward;
            if (value < 500)
                value = 500;
            return value;
        }

        public int ExperienceToNextLevel() =>
            ExperienceToLevel(SharedData.SaveData.PlayerLevel + 1) - ExperienceToLevel(SharedData.SaveData.PlayerLevel);

        public int ExperienceToLevel(int level) =>
            (int)(SharedData.BalanceData.LevelBaseCoef * Mathf.Pow(SharedData.BalanceData.LevelMultiCoef, level));

        /*public void SetState(GameStateType stateType)
        {
            PreviousGameStateType = CurrentGameStateType;
            CurrentGameStateType = stateType;
            
            //if()
        }*/
        
        
        public void UpdateStatsByIngredients(ref Stats stats, ref int saveId)
        {
            foreach (var stat in stats.Value)
                stat.Value.RemoveAllModifiers();

            Debug.Log($"saveId{saveId} {SharedData.SaveData.TadpoleSaveData[saveId].TadpoleName}");
            if (SharedData.SaveData.TadpoleSaveData[saveId].Ingredients != null)
                foreach (var ingredient in SharedData.SaveData.TadpoleSaveData[saveId].Ingredients)
                {
                    var ingredientData = SharedData.StaticData.IngredientDataByType[ingredient.Key];
                    var statModifier = new StatModifier()
                    {
                        Type = ingredientData.StatModifierType,
                        Value = ingredientData.GetStatModifierValueByLevel(ingredient.Value)
                    };
                    stats.Value[ingredientData.StatType].AddModifier(statModifier);
                    stats.Value[ingredientData.StatType].SaveThisUpgradeModifier();
                }

            stats.Value[StatType.Fat].SetAllValues(SharedData.SaveData.TadpoleSaveData[saveId].FatAmount);
        }
    }

    public enum RaceStep
    {
        Sprint = 0,
        Labyrinth = 1,
        FinalRace = 2
    }
}
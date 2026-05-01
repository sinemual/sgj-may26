using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Client.Data;
using Client.Data.Core;
using Client.Data.Equip;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class SaveData
    {
        [Header("GameSettings")] public bool IsGameLaunchedBefore;
        public bool IsVibrationOn;
        public bool IsMusicOn;
        public bool IsEffectsOn;
        public float MusicVolume;
        public float EffectsVolume;

        [Header("Currency")] public int Currency;
        public int LevelIdx;
        public int EventLevelIdx;
        public int PlayerLevel;
        public float Experience;
        public int CatchCounter;
        public int Day;

        [Header("Timers")] public string IdleRewardTimeKey;

        public TutorialStep CurrentTutorialStep;

        [Header("Tutorials")] public SerializedDictionary<TutorialStep, bool> TutrorialStates;

        [Header("CurrentGame")] public SerializedDictionary<string, TadpoleSaveData> TadpoleSaveData;
        public SerializedDictionary<ItemType, int> Ingredients;

        public void ResetToDefaults()
        {
            LevelIdx = 0;
            EventLevelIdx = 0;
            PlayerLevel = 1;
            Experience = 0;
            CatchCounter = 0;

            IsGameLaunchedBefore = false;
            IsVibrationOn = true;
            IsMusicOn = true;
            IsEffectsOn = true;
            MusicVolume = 0.5f;
            EffectsVolume = 0.5f;

            CurrentTutorialStep = 0;
            IdleRewardTimeKey = "";

            TutrorialStates = new SerializedDictionary<TutorialStep, bool>();
            TadpoleSaveData = new SerializedDictionary<string, TadpoleSaveData>();
            Ingredients = new SerializedDictionary<ItemType, int>();
        }

        public void BindData(int startMoney, List<TutorialData> tutorialDatas, List<ItemData> itemDatas)
        {
            Currency = startMoney;
            for (var i = 0; i < tutorialDatas.Count; i++)
                TutrorialStates.Add((TutorialStep)i, false);

            foreach (var itemData in itemDatas)
            {
                Debug.Log($"itemData.ItemType: {itemData.ItemType}");
                Ingredients.Add(itemData.ItemType, 0);
            }
        }
    }

    [Serializable]
    public class TadpoleSaveData
    {
        public int DataId;
        public string TadpoleName;
    }
}
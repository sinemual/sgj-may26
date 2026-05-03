using System;
using AYellowpaper.SerializedCollections;
using Data.Base;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "GameData/LevelData", fileName = "LevelData")]
    [Serializable]
    public class LevelData : BaseDataSO
    {
        public int Id;
        public GameObject Prefab;
    }
    
    [CreateAssetMenu(menuName = "GameData/TextData", fileName = "TextData")]
    [Serializable]
    public class TextData : BaseDataSO
    {
        public SerializedDictionary<TextType, string> Texts;
    }

    public enum TextType
    {
        None = 0,
        Intro = 1,
        Lavender = 2,
        PineCone = 3,
        Dung = 4,
        Mushrooms = 5,
        Food = 6,
        Outro = 100 
    }
}
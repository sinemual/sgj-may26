using System;
using AYellowpaper.SerializedCollections;
using Client.Data.Core;
using Data.Base;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "GameData/TextData", fileName = "TextData")]
    [Serializable]
    public class TextData : BaseDataSO
    {
        public SerializedDictionary<TextType, string> Texts;
        public SerializedDictionary<IngredientType, string> TextByIngredientType;
    }
}
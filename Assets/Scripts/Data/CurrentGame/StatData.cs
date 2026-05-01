using System;
using Client.Data.Core;
using Data.Base;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "GameData/StatData", fileName = "StatData")]
    [Serializable]
    public class StatData : BaseDataSO
    {
        public StatType StatType;
        public bool IsPercent;
        public Sprite StatSprite;
        public string StatName;
        public string StatShortName;
    }
}
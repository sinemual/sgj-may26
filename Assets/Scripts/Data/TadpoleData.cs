using System;
using Data.Base;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "GameData/TadpoleData", fileName = "TadpoleData")]
    [Serializable]
    public class TadpoleData : BaseDataSO
    {
        public int Id;
        public GameObject Prefab;
    }
}
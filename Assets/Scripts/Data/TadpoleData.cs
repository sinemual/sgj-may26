using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Client;
using Client.Data.Core;
using Client.ECS.CurrentGame.Player;
using Data.Base;
using TriInspector;
using UnityEditor;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "GameData/TadpoleData", fileName = "TadpoleData")]
    [Serializable]
    public class TadpoleData : BaseDataSO
    {
        public TadpoleType TadpoleType;
        public GameObject Prefab;
        public GameObject CaviarPrefab;
        public SerializedDictionary<StatType, Stat> Stats;
        
#if UNITY_EDITOR
        [Button]
        public new void SetDirty()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [Button]
        public void Log()
        {
            var json = JsonUtility.ToJson(this);
            Debug.Log(json);
        }
#endif
    }
}
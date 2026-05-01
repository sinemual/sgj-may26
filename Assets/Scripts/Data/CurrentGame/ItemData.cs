using System;
using Client.Data.Core;
using TriInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Client.Data.Equip
{
    [CreateAssetMenu(menuName = "SharedData/ItemData", fileName = "ItemData")]
    [Serializable]
    public class ItemData : ScriptableObject
    {
        public string Id;
        public ItemType ItemType;
        public Rarity Rarity;
        public string ItemName;
        public string ItemDescription;
        public int Price;
        public int Level;
        public bool IsNotSale;
        public ItemView ItemView;
        public Vector3 Size;

        private void OnValidate() => Id = name.ToLower();


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
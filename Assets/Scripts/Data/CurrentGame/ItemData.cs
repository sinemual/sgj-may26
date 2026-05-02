using System;
using Client.Data.Core;
using Client.ECS.CurrentGame.Player;
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
        public IngredientType IngredientType;
        public Rarity Rarity;
        public string ItemName;
        public string ItemDescription;
        public int Price;
        public int Level;
        public bool IsNotSale;
        public ItemView ItemView;
        public Vector3 Size;
        [Header("Effect")]
        public StatType StatType;
        public StatModifierType StatModifierType;
        public BalanceCurve StatModifierValue;
        
        private void OnValidate() => Id = name.ToLower();

        public float GetStatModifierValueByLevel(int level)
        {
            return StatModifierValue.GetValueByLevel(level);
        }

        public StatModifier GetStatModifierByLevel(ref StatModifier statModifier, int level)
        {
            statModifier.Type = StatModifierType;
            statModifier.Value = GetStatModifierValueByLevel(level);
            return statModifier;
        }

        public float GetStatModifierValueByNextLevel(int level)
        {
            return StatModifierValue.GetValueByLevel(level + 1);
        }

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
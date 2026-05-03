using System;
using System.Collections.Generic;
using Client.Data.Equip;
using Client.Infrastructure.UI.BaseUI;
using Data;
using UnityEngine;

public class IngredientsPanel : MonoBehaviour
{
    [SerializeField] private IngredientPanel[] panels;

    public event Action<ItemData> AddIngredient;

    public void InitIngredient(int num, ItemData itemData, int amount)
    {
        panels[num].PickIngredient -= OnAddIngredient;
        panels[num].UpdateInfoByData(itemData, amount);
        panels[num].PickIngredient += OnAddIngredient;
    }

    private void OnAddIngredient(ItemData data)
    {
        Debug.Log($"OnAddIngredient {data}");
        AddIngredient?.Invoke(data);
    }
}
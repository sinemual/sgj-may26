using System;
using Client.Data.Equip;
using Client.Infrastructure.UI.BaseUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPanel : MonoBehaviour
{
    [SerializeField] private Image panelImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private UIButton pickButton;
    [SerializeField] private GameObject coverGo;
    [SerializeField] private RectTransform rectT;

    public event Action<ItemData> PickIngredient;
    private ItemData _ingredientData;

    public RectTransform RectT => rectT;

    public void UpdateInfoByData(ItemData itemData, int amount)
    {
        _ingredientData = itemData;
        bool isCanPickIt = amount >= 0;
        iconImage.sprite = _ingredientData.ItemView.ItemSprite;
        nameText.text = $"{_ingredientData.ItemName}";
        amountText.text = $"{amount}";

        pickButton.Clicked -= PickUpgradeClick;
        pickButton.SetInteractable(isCanPickIt);
        if (isCanPickIt)
            pickButton.Clicked += PickUpgradeClick;

        panelImage.gameObject.SetActive(amount != 0);

        coverGo.SetActive(!isCanPickIt);
    }

    private void OnDisable()
    {
        pickButton.Clicked -= PickUpgradeClick;
    }

    private void PickUpgradeClick()
    {
        PickIngredient?.Invoke(_ingredientData);
    }
}
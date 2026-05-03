using System;
using System.Collections.Generic;
using Client.Data.Core;
using Client.Data.Equip;
using Client.Infrastructure.UI.BaseUI;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : BaseScreen
{
    [SerializeField] private TextMeshProUGUI tadpoleNameText;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private UIButton feedButton;
    [SerializeField] private UIButton flushButton;
    [SerializeField] private UIButton addIngredientButton;
    [SerializeField] private UIButton sleepButton;
    [SerializeField] private UIButton nextButton;
    [SerializeField] private UIButton previousButton;
    [SerializeField] private IngredientsPanel ingredientsPanel;

    [SerializeField] private UIButton startRaceButton;
    [SerializeField] private UIButton goToGatheringButton;
    [SerializeField] private UIButton goToCatchingButton;
    
    public event Action FeedButtonClick;
    public event Action FlushButtonClick;
    public event Action SleepButtonClick;
    public event Action<ItemData> AddIngredientButtonClick;
    public event Action NextButtonClick;
    public event Action PreviousButtonButtonClick;
    public event Action StartRaceButtonClick;
    public event Action GoToGatheringButtonClick;
    public event Action GoToCatchingButtonClick;
    
    public UIButton StartRaceButton => startRaceButton;

    public UIButton GoToGatheringButton => goToGatheringButton;

    public UIButton GoToCatchingButton => goToCatchingButton;


    protected override void ManualStart()
    {
        feedButton.Clicked += OnFeedButtonClick;
        flushButton.Clicked += OnFlushButtonClick;
        sleepButton.Clicked += OnSleepButtonClick;
        //addIngredientButton.Clicked += OnAddIngredientButtonClick;
        StartRaceButton.Clicked += OnStartRaceButtonClick;
        GoToGatheringButton.Clicked += OnGoToGatheringButtonClick;
        GoToCatchingButton.Clicked += OnGoToCatchingButtonClick;
        nextButton.Clicked += OnNextButtonClick;
        previousButton.Clicked += OnPreviousButtonButtonClick;

        ShowScreen += UpdateView;
    }

    private void UpdateView()
    {
    }

    private void OnFeedButtonClick() => FeedButtonClick?.Invoke();
    private void OnFlushButtonClick() => FlushButtonClick?.Invoke();
    private void OnSleepButtonClick() => SleepButtonClick?.Invoke();
    //private void OnAddIngredientButtonClick() => AddIngredientButtonClick?.Invoke();
    private void OnNextButtonClick() => NextButtonClick?.Invoke();
    private void OnPreviousButtonButtonClick() => PreviousButtonButtonClick?.Invoke();
    private void OnStartRaceButtonClick() => StartRaceButtonClick?.Invoke();
    private void OnGoToGatheringButtonClick() => GoToGatheringButtonClick?.Invoke();
    private void OnGoToCatchingButtonClick() => GoToCatchingButtonClick?.Invoke();

    public void UpdateIngredients(Dictionary<IngredientType, int> playerIngredients, List<ItemData> allIngredients)
    {
        int k = 0;
        foreach (var ingredient in allIngredients)
        {
            ingredientsPanel.AddIngredient -= AddIngredientButtonClick;
            ingredientsPanel.InitIngredient(k, ingredient, playerIngredients[ingredient.IngredientType]);
            ingredientsPanel.AddIngredient += AddIngredientButtonClick;
            k += 1;
        }
    }

    public void UpdateTadpoleNameText(string tadpoleName)
    {
        tadpoleNameText.text = tadpoleName == "Empty" ? $"Пусто" : $"\"{tadpoleName}\"";

        Tween.PunchScale(
            target: tadpoleNameText.transform,
            strength: Vector3.one * 0.1f,
            duration: 0.15f
        );
    }

    public void UpdateNumberText(string number/*, int amount*/)
    {
        numberText.text = $"#{number}";

        Tween.PunchScale(
            target: numberText.transform,
            strength: Vector3.one * 0.1f,
            duration: 0.15f
        );
    }
}
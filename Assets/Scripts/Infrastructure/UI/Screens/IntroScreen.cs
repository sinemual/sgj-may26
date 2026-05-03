using System;
using Client.Infrastructure.UI.BaseUI;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroScreen : BaseScreen
{
    [SerializeField] private UIButton skipButton;

    public event Action SkipButtonClick;
    protected override void ManualStart()
    {
        skipButton.Clicked += OnSkipButtonClick;
        ShowScreen += UpdateView;
    }

    private void UpdateView()
    {
    }

    private void OnSkipButtonClick() => SkipButtonClick?.Invoke();

    /*public void UpdateMoneyText(int moneyAmount)
    {
        //moneyText.text = $"{Utility.Format(moneyCount)}"; // money sprite
        int currentMoney = moneyAmount;

        Tween.Custom(
            startValue: (float)currentMoney,
            endValue: (float)moneyAmount,
            duration: 0.3f,
            onValueChange: value => moneyText.text = $"{value:0}",
            ease: Ease.OutQuad
        );

        Tween.PunchScale(
            target: moneyText.transform,
            strength: Vector3.one * 0.1f,
            duration: 0.15f
        );
    }

    public void UpdateLevelText(int level) => levelText.text = $"Level {level + 1}";
    public void UpdateLevelText(string levelName) => levelText.text = $"{levelName}";*/
}
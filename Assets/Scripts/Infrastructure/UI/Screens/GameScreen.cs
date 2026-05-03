using System;
using System.Collections.Generic;
using Client;
using Client.DevTools.MyTools;
using Client.Infrastructure.UI.BaseUI;
using Data;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : BaseScreen
{
    //[SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI textText;
    [SerializeField] private GameObject textPanel;
    [SerializeField] private Image sleepImage;
    [SerializeField] private UIButton pauseButton;
    
    [SerializeField] private UIButton startRaceButton;
    [SerializeField] private UIButton goToGatheringButton;
    [SerializeField] private UIButton goToCatchingButton;
    [SerializeField] private UIButton goToHomeButton;
    
    /*[Header("Level Progress Bar")] [SerializeField]
    private TextMeshProUGUI levelText;

    [SerializeField] private Image levelProgressMoveBarFill;*/

    public event Action PauseButtonClick;
    public event Action StartRaceButtonClick;
    public event Action GoToGatheringButtonClick;
    public event Action GoToCatchingButtonClick;
    public event Action GoToHomeButtonClick;

    public UIButton StartRaceButton => startRaceButton;

    public UIButton GoToGatheringButton => goToGatheringButton;

    public UIButton GoToCatchingButton => goToCatchingButton;

    public UIButton GoToHomeButton => goToHomeButton;

    protected override void ManualStart()
    {
        pauseButton.Clicked += OnPauseButtonClick;
        StartRaceButton.Clicked += OnStartRaceButtonClick;
        GoToGatheringButton.Clicked += OnGoToGatheringButtonClick;
        GoToCatchingButton.Clicked += OnGoToCatchingButtonClick;
        GoToHomeButton.Clicked += OnGoToHomeButtonClick;
        ShowScreen += UpdateView;
    }

    private void UpdateView()
    {
    }

    private void OnPauseButtonClick() => PauseButtonClick?.Invoke();
    private void OnStartRaceButtonClick() => StartRaceButtonClick?.Invoke();
    private void OnGoToGatheringButtonClick() => GoToGatheringButtonClick?.Invoke();
    private void OnGoToCatchingButtonClick() => GoToCatchingButtonClick?.Invoke();
    private void OnGoToHomeButtonClick() => GoToHomeButtonClick?.Invoke();

    public void UpdateMoneyText(int moneyAmount)
    {
        /*//moneyText.text = $"{Utility.Format(moneyCount)}"; // money sprite
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
        );*/
    }
    
    public void UpdateDayText(int dayNum)
    {
        dayText.text = $"День: {dayNum}";

        Tween.PunchScale(
            target: dayText.transform,
            strength: Vector3.one * 0.1f,
            duration: 0.15f
        );
    }

    public void ShowTextPanel(string text)
    {
        textPanel.SetActive(true);
        textText.text = text;
    }
    
    public void HideTextPanel()
    {
        textPanel.SetActive(false);
    }
    
    public void Sleep()
    {
        Tween.Alpha(sleepImage, 1.0f, new TweenSettings(0.5f, Ease.InCubic, 2, CycleMode.Yoyo));
    }

    /*public void UpdateLevelText(int level) => levelText.text = $"Level {level + 1}";
    public void UpdateLevelText(string levelName) => levelText.text = $"{levelName}";*/
}
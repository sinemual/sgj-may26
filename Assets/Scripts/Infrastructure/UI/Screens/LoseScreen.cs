using System;
using Client.Infrastructure.UI.BaseUI;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : BaseScreen
{
    [SerializeField] private TextMeshProUGUI placeInRaceText;
    [SerializeField] private UIButton goToHomeButton;

    public event Action GoToHomeButtonClick;
    protected override void ManualStart()
    {
        goToHomeButton.Clicked += OnGoToHomeButtonClick;
        ShowScreen += UpdateView;
    }

    private void UpdateView()
    {
    }

    private void OnGoToHomeButtonClick() => GoToHomeButtonClick?.Invoke();

    public void UpdatePlaceInRaceText(int place, int finishers)
    {
        placeInRaceText.text = $"{place}/{finishers}";

        Tween.PunchScale(
            target: placeInRaceText.transform,
            strength: Vector3.one * 0.1f,
            duration: 0.15f
        );
    }
}
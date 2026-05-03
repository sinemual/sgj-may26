using System;
using Client.Infrastructure.UI.BaseUI;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutroScreen : BaseScreen
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
}
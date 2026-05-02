using System;
using Client.Infrastructure.UI.BaseUI;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : BaseScreen
{
    [SerializeField] private UIButton goToHomeButton;
    [SerializeField] private UIButton nextStepButton;

    public event Action GoToHomeButtonClick;
    public event Action NextStepButtonClick;

    public UIButton GoToHomeButton => goToHomeButton;
    public UIButton NextStepButton => nextStepButton;

    protected override void ManualStart()
    {
        GoToHomeButton.Clicked += OnGoToHomeButtonClick;
        NextStepButton.Clicked += OnNextStepButtonClick;
        ShowScreen += UpdateView;
    }

    private void UpdateView()
    {
    }
    
    private void OnGoToHomeButtonClick() => GoToHomeButtonClick?.Invoke();
    private void OnNextStepButtonClick() => NextStepButtonClick?.Invoke();
    
}
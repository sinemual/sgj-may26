using System;
using Client;
using Client.Infrastructure.UI.BaseUI;
using UnityEngine;

public class ExampleScreen : BaseScreen
{
    [SerializeField] private IngredientsPanel ingredientsPanel;
    [SerializeField] private UIButton closeButton;
    [SerializeField] private UIButton startExperimentButton;
    [SerializeField] private MovablePanel movablePanel;
    [SerializeField] private MonoEntity tutorialButtonEntity;
    [SerializeField] private TutorialHandAnimation tutorialHandAnimation;
    public event Action CloseButtonClick;
    public event Action StartExperimentButtonClick;
    public IngredientsPanel IngredientPanels => ingredientsPanel;
    public MonoEntity TutorialButtonEntity => tutorialButtonEntity;
    protected override void ManualStart()
    {
        closeButton.Clicked += OnCloseButtonClick;
        startExperimentButton.Clicked += OnStartExperimentButtonClick;
        ShowScreen += UpdateView;
        movablePanel.OnMoveToEndComplete += OnMoveToEndComplete;
    }

    private void UpdateView()
    {
    }

    protected override void Show()
    {
        gameObject.SetActive(true);
        //base.Show();
        movablePanel.MoveToEnd();
    }

    protected override void Hide()
    {
        //base.Hide();
        tutorialHandAnimation.gameObject.SetActive(false);
        movablePanel.MoveToStart();
    }

    private void OnMoveToEndComplete()
    {
        gameObject.SetActive(true);
    }

    private void OnCloseButtonClick() => CloseButtonClick?.Invoke();
    private void OnStartExperimentButtonClick() => StartExperimentButtonClick?.Invoke();
}
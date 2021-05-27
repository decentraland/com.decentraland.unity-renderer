using System;
using UnityEngine;
using UnityEngine.UI;

public interface IExtraActionsView
{
    event Action OnControlsClicked,
                 OnHideUIClicked,
                 OnTutorialClicked,
                 OnResetClicked,
                 OnResetCameraClicked;

    void OnControlsClick(DCLAction_Trigger action);
    void OnHideUIClick(DCLAction_Trigger action);
    void OnTutorialClick();
    void SetActive(bool isActive);
    void OnResetClick();
    void OnResetCameraClick();
}

public class ExtraActionsView : MonoBehaviour, IExtraActionsView
{
    public event Action OnControlsClicked,
                        OnHideUIClicked,
                        OnTutorialClicked,
                        OnResetClicked,
                        OnResetCameraClicked;

    [Header("Buttons")]
    [SerializeField] internal Button hideUIBtn;
    [SerializeField] internal Button controlsBtn;
    [SerializeField] internal Button tutorialBtn;
    [SerializeField] internal Button resetBtn;
    [SerializeField] internal Button resetCameraBtn;

    [Header("Input Actions")]
    [SerializeField] internal InputAction_Trigger toggleUIVisibilityInputAction;
    [SerializeField] internal InputAction_Trigger toggleControlsVisibilityInputAction;

    private DCLAction_Trigger dummyActionTrigger = new DCLAction_Trigger();

    private const string VIEW_PATH = "GodMode/TopActionsButtons/ExtraActionsView";

    internal static ExtraActionsView Create()
    {
        var view = Instantiate(Resources.Load<GameObject>(VIEW_PATH)).GetComponent<ExtraActionsView>();
        view.gameObject.name = "_ExtraActionsView";

        return view;
    }

    private void Awake()
    {
        hideUIBtn.onClick.AddListener(() => OnHideUIClick(dummyActionTrigger));
        controlsBtn.onClick.AddListener(() => OnControlsClick(dummyActionTrigger));
        resetBtn.onClick.AddListener(OnResetClick);
        resetCameraBtn.onClick.AddListener(OnResetCameraClick);
        tutorialBtn.onClick.AddListener(OnTutorialClick);

        toggleUIVisibilityInputAction.OnTriggered += OnHideUIClick;
        toggleControlsVisibilityInputAction.OnTriggered += OnControlsClick;
    }

    private void OnDestroy()
    {
        hideUIBtn.onClick.RemoveAllListeners();
        controlsBtn.onClick.RemoveAllListeners();
        resetBtn.onClick.RemoveAllListeners();
        resetCameraBtn.onClick.RemoveAllListeners();
        tutorialBtn.onClick.RemoveListener(OnTutorialClick);

        toggleUIVisibilityInputAction.OnTriggered -= OnHideUIClick;
        toggleControlsVisibilityInputAction.OnTriggered -= OnControlsClick;
    }

    public void SetActive(bool isActive)
    {
        if (gameObject.activeSelf != isActive)
            gameObject.SetActive(isActive);
    }

    public void OnControlsClick(DCLAction_Trigger action) { OnControlsClicked?.Invoke(); }

    public void OnHideUIClick(DCLAction_Trigger action) { OnHideUIClicked?.Invoke(); }

    public void OnTutorialClick() { OnTutorialClicked?.Invoke(); }

    public void OnResetClick() { OnResetClicked?.Invoke(); }

    public void OnResetCameraClick() { OnResetCameraClicked?.Invoke(); }
}
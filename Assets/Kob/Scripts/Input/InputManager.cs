
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    #region Singleton
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(InputManager)) as InputManager;

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static InputManager instance;
    #endregion

    private CustomInput actions;

    [HideInInspector] public static event Action rebindComplete;
    [HideInInspector] public static event Action rebindCanceled;
    [HideInInspector] public static event Action<InputAction, int> rebindStarted;

    private void Awake()
    {
        if(actions == null) actions = new CustomInput();

        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        actions.Player.Enable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 moveVector = actions.Player.Move.ReadValue<Vector2>();

        return moveVector;
    }

    public Vector2 GetLookVector()
    {
        Vector2 lookVector = actions.Player.Look.ReadValue<Vector2>();

        return lookVector;
    }


    public bool GetJumpFlag()
    {
        bool jumpFlag = actions.Player.Jump.ReadValue<float>() > 0;

        return jumpFlag;
    }

    public bool GetClimbFlag()
    {
        bool climbFlag = actions.Player.Climb.ReadValue<float>() > 0;

        return climbFlag;
    }

    public bool GetGrabLeftFlag()
    {
        bool grabLeftFlag = actions.Player.GrabLeft.ReadValue<float>() > 0;

        return grabLeftFlag;
    }

    public bool GetGrabRightFlag()
    {
        bool grabRightFlag = actions.Player.GrabRight.ReadValue<float>() > 0;

        return grabRightFlag;
    }

    public bool GetSprintFlag()
    {
        bool sprintFlag = actions.Player.Sprint.ReadValue<float>() > 0;

        return sprintFlag;
    }

    public bool GetTugFlag()
    {
        bool tugFlag = actions.Player.Tug.triggered;

        return tugFlag;
    }

    #region REBINDING LOGIC

    public void StartRebind(string actionName, int bindingIndex, TMP_Text statusText, bool excludeMouse)
    {
        InputAction action = actions.asset.FindAction(actionName);
        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Couldn't find action or binding");
            return;
        }

        if (action.bindings[bindingIndex].isComposite)
        {
            var firstPartIndex = bindingIndex + 1;
            if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isComposite)
                DoRebind(action, bindingIndex, statusText, true, excludeMouse);
        }
        else
            DoRebind(action, bindingIndex, statusText, false, excludeMouse);
    }

    private void DoRebind(InputAction actionToRebind, int bindingIndex, TMP_Text statusText, bool allCompositeParts, bool excludeMouse)
    {
        if (actionToRebind == null || bindingIndex < 0)
            return;

        statusText.text = $"[PRESS A KEY]";

        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            if (allCompositeParts)
            {
                var nextBindingIndex = bindingIndex + 1;
                if (nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
                    DoRebind(actionToRebind, nextBindingIndex, statusText, allCompositeParts, excludeMouse);
            }

            SaveBindingOverride(actionToRebind);
            rebindComplete?.Invoke();
        });

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            rebindCanceled?.Invoke();
        });

        rebind.WithCancelingThrough("<Keyboard>/escape");

        if (excludeMouse)
            rebind.WithControlsExcluding("Mouse");

        rebindStarted?.Invoke(actionToRebind, bindingIndex);
        rebind.Start(); //actually starts the rebinding process
    }

    public string GetBindingName(string actionName, int bindingIndex)
    {
        if (actions == null)
            actions = new CustomInput();

        InputAction action = actions.asset.FindAction(actionName);
        return action.GetBindingDisplayString(bindingIndex);
    }

    private static void SaveBindingOverride(InputAction action)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
    }

    public void LoadBindingOverride(string actionName)
    {
        if (actions == null)
            actions = new CustomInput();

        InputAction action = actions.asset.FindAction(actionName);

        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
        }
    }

    public void ResetBinding(string actionName, int bindingIndex)
    {
        InputAction action = actions.asset.FindAction(actionName);

        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Could not find action or binding");
            return;
        }

        if (action.bindings[bindingIndex].isComposite)
        {
            for (int i = bindingIndex; i < action.bindings.Count && action.bindings[i].isComposite; i++)
                action.RemoveBindingOverride(i);
        }
        else
            action.RemoveBindingOverride(bindingIndex);

        SaveBindingOverride(action);
    }

    #endregion
}

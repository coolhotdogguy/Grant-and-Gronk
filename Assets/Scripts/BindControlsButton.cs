using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BindControlsButton : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public InputActionReference mainAction;
    public int index;

    InputActionRebindingExtensions.RebindingOperation rebindOperation;

    public void Remap(string part)
    {
        text.text = "Press a key";

        rebindOperation = mainAction.action.PerformInteractiveRebinding(index)
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .WithCancelingThrough("<Keyboard>/escape")
                    .WithTargetBinding(1)
                    .OnMatchWaitForAnother(0.2f)
                    .Start()
                    .OnApplyBinding(
                        (op, path) =>
                        {
                            text.text = path;
                        });
    }
}

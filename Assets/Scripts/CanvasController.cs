using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasController : MonoBehaviour
{
    public GameObject canvasToToggle;
    public InputActionReference toggleAction;

    private void OnEnable()
    {
        toggleAction.action.performed += ToggleCanvas;
        toggleAction.action.Enable();
    }

    private void OnDisable()
    {
        toggleAction.action.performed -= ToggleCanvas;
        toggleAction.action.Disable();
    }

    private void ToggleCanvas(InputAction.CallbackContext context)
    {
        if (canvasToToggle != null)
        {
            canvasToToggle.SetActive(!canvasToToggle.activeSelf);
        }
    }
}

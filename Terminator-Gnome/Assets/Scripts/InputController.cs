using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InputController : MonoBehaviour
{
    public static InputController Instance;
    public event Action<Vector2> OnMoveInput;
    public event Action<bool> OnShiftPressed;
    public event Action OnRightClickPressed;

    void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMoveInput?.Invoke(moveInput.normalized);
        if (Input.GetKeyDown(KeyCode.LeftShift) && moveInput != Vector2.zero)
        {
            Debug.Log("SHIFT");
            OnShiftPressed?.Invoke(true);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnRightClickPressed?.Invoke();
        }

    }
}

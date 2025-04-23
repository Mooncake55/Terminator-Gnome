using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InputController : MonoBehaviour
{
    public static InputController Instance;
    public event Action<Vector2> OnMoveInput;
    public event Action<bool> OnShiftPressed;

    void Awake()
    {
        Instance = this;
    }
    private void FixedUpdate()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMoveInput?.Invoke(moveInput.normalized);
        if (Input.GetKey(KeyCode.LeftShift))
        { 
            OnShiftPressed?.Invoke(true);
            Debug.Log("Shift");
        }
    }


}

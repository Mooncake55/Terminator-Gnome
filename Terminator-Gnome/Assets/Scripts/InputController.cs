using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InputController : MonoBehaviour
{
    public static InputController Instance;
    public event Action<Vector2> OnMoveInput;
    public event Action<Vector2, bool> OnShiftPressed;

    void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        //Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //OnMoveInput?.Invoke(moveInput.normalized);
        //if (Input.GetKey(KeyCode.LeftShift))
        //{ 
        //    OnShiftPressed?.Invoke(moveInput.normalized, true);
        //    Debug.Log("Shift");
        //}
        bool isLeftShiftPressed = false;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isLeftShiftPressed = true;
        }
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (isLeftShiftPressed)
        {
            OnShiftPressed?.Invoke(moveInput.normalized, true);

        }
        else
        {
            OnMoveInput?.Invoke(moveInput.normalized);
        }


    }
}

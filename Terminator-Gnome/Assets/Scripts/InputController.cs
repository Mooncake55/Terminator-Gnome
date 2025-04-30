using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InputController : MonoBehaviour
{
    public static InputController Instance;
    public event Action<Vector2> OnMoveInput;
    public event Action<bool> OnShiftPressed;
    //public event Action<Vector2, bool> OnLeftClickPressed;

    void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        //bool isLeftShiftPressed = false;
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    isLeftShiftPressed = true;
        //}
        //Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //if (isLeftShiftPressed)
        //{
        //    OnShiftPressed?.Invoke(moveInput.normalized, true);

        //}
        //else
        //{
        //    OnMoveInput?.Invoke(moveInput.normalized);
        //}
        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    OnLeftClickPressed?.Invoke(moveInput.normalized, false);
        //}
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMoveInput?.Invoke(moveInput.normalized);
        if (Input.GetKeyDown(KeyCode.LeftShift) && moveInput != Vector2.zero)
        {
            OnShiftPressed?.Invoke(true);
        }



    }
}

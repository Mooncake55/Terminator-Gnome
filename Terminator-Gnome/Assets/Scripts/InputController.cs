using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InputController : MonoBehaviour
{
    public static InputController Instance;
    public event Action<Vector2> OnMoveInput;

    void Awake()
    {
        Instance = this;
    }
    private void FixedUpdate()
    {
        //Debug.Log("Hello");
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(moveInput != Vector2.zero)
        {
            Debug.Log("Should Move");
            OnMoveInput?.Invoke(moveInput.normalized);
        }
    }

}

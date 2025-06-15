using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InputController : MonoBehaviour
{
    //this class is a Singleton
    public static InputController instance;

    public event Action<Vector2> OnMoveInput;
    public event Action<bool> OnShiftPressed;
    public event Action OnLeftClickPressed;
    public event Action OnRightClickPressed;


    public Vector2 mouseWorldPos;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //notifies the diferent user�s inputs through events
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
            OnLeftClickPressed?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            OnRightClickPressed?.Invoke();
        }
    }

    public Vector2 GetMousePos()
    {
        return mouseWorldPos;
    }
}

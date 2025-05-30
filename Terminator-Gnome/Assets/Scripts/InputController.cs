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
    public event Action OnRightClickPressed;

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

    //notifies the diferent user´s inputs through events
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

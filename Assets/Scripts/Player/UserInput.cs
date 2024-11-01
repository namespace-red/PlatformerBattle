using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);

    public float HorizontalInput { get; private set; }
    
    public event Action JumpPressed;
    
    private void Update()
    {
        HorizontalInput = Input.GetAxisRaw(Horizontal);
        
        if (Input.GetButtonDown(Jump))
            JumpPressed?.Invoke();
    }
}

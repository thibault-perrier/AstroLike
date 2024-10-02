using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _col;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    //         private bool _jumpToConsume;
    //         private bool _bufferedJumpUsable;
    //         private bool _endedJumpEarly;
    //         private bool _coyoteUsable;
    //         private float _timeJumpWasPressed;

    //         private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
    //         private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

    //         private void HandleJump()
    //         {
    //             if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

    //             if (!_jumpToConsume && !HasBufferedJump) return;

    //             if (_grounded || CanUseCoyote) ExecuteJump();

    //             _jumpToConsume = false;
    //         }


    public void OnMove(InputAction.CallbackContext context) // fixed update -> put a bool that allow to know if player is moving or not
    {
        if (context.performed)
        {
            float movement = context.ReadValue<float>();

            if (movement > 0)
            {
                Debug.Log("Moving to the right");
            }
            else if (movement < 0)
            {
                Debug.Log("Moving to the left");
            }
        }
        else if (context.canceled)
        {
            Debug.Log("Stopping movement");
        }
    }

    public void OnJump(InputAction.CallbackContext context) // make jump -> test if can jump -> is grounded (stays true for .15s after stepping out of a platform) + jump buffer
    {
        if (context.performed)
        {
            Debug.Log("IS JUMPING");
        }
    }

    public void OnAttack1(InputAction.CallbackContext context) // get access to the weapon and use its first attack
    {
        if (context.performed)
        {
            Debug.Log("IS USING ATTACK 1");
        }
    }

    public void OnAttack2(InputAction.CallbackContext context) // get access to the weapon and use its first attack
    {
        if (context.performed)
        {
            Debug.Log("IS USING ATTACK 2");
        }
    }
}

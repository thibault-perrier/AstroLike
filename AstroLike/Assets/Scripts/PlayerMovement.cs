using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _col;
    public Vector3 _playerPos;


    [Header("Layers")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Physics")]
    [SerializeField] private float _jumpDetectionRadius;
    private float _gravityConstant = 9.81f;


    [Header("Player Movement Stats")]
    [SerializeField] private float _playerMovSpeed;
    [SerializeField] private float _playerJumpForce;
    private float _playerDir = 0.0f;

    [Header("Player Jump Stats")]
    [SerializeField] private float _coyoteTime;
    [SerializeField] private float _jumpBufferTime;
    private bool _canJumpFromPlatform => IsOnGround || IsOnLeftWall || IsOnRightWall;
    private bool _jumpToConsume = false;
    private bool _coyoteTimeJumpUsable = false;
    private bool _hasJustJumped = false;
    [SerializeField] private float _lowGravityTimer;
    [SerializeField] private float _lowGravityScale;
    private float _normalGravityScale;


    [Header("Collision Detection")]
    private PlatformDetection leftDetection;
    private PlatformDetection rightDetection;
    private PlatformDetection downDetection;
    // delegates
    // is on platform
    private bool IsOnLeftWall => leftDetection._isGrounded;
    private bool IsOnRightWall => rightDetection._isGrounded;
    private bool IsOnGround => downDetection._isGrounded;
    // has just left
    private bool HasJustLeftLW
    {
        get { return leftDetection._hasJustLeftPlatform; }
        set { leftDetection._hasJustLeftPlatform = value; }
    }
    private bool HasJustLeftRW
    {
        get { return rightDetection._hasJustLeftPlatform; }
        set { rightDetection._hasJustLeftPlatform = value; }
    }
    private bool HasJustLeftGr
    {
        get { return downDetection._hasJustLeftPlatform; }
        set { downDetection._hasJustLeftPlatform = value; }
    }


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
        _playerPos = transform.position;

        _normalGravityScale = _rb.gravityScale;
    }

    void Update()
    {
        if (HasJustLeftGr)
        {
            // Do Check Has Just Jumped
        }
        if (HasJustLeftLW)
        {
            // Do Check Has Just Jumped
        }
        if (HasJustLeftRW)
        {
            // Do Check Has Just Jumped
        }
    }


    void FixedUpdate()
    {
        GetComponent<SpriteRenderer>().color = IsOnGround || IsOnLeftWall || IsOnRightWall || _coyoteTimeJumpUsable ? Color.red : Color.blue;


        float horizontalMov = _playerDir == 0 ? 0.0f : _playerDir * _playerMovSpeed;
        _rb.velocity = new Vector2(horizontalMov, _rb.velocity.y);
    }

    private IEnumerator HandleJumpBuffer()
    {
        float timer = 0.0f;
        while ((timer += Time.deltaTime) < _jumpBufferTime)
        {
            if (_jumpToConsume && _canJumpFromPlatform)
            {
                DoJump();
                Debug.Log("JUMP BUFFER USED");
                break;
            }
            yield return null;
        }

        _jumpToConsume = false;
        yield return null;
    }

    private IEnumerator HandleCoyoteTime()
    {
        _rb.gravityScale = 0.0f;
        _coyoteTimeJumpUsable = true;

        yield return new WaitForSeconds(_coyoteTime);

        _rb.gravityScale = _normalGravityScale;
        _coyoteTimeJumpUsable = false;
        yield return null;
    }

    private IEnumerator HandleLowGravityAtJumpPeak()
    {
        float t_vertex = _rb.velocity.y / (_gravityConstant * _lowGravityScale);
        float lowGStartTimer = t_vertex - (_lowGravityTimer / 2);

        // Debug.Log("t_vertex: " + t_vertex + ", low G (start, end) timer: (" + lowGStartTimer + ", " + (lowGStartTimer + _lowGravityTimer) + "). ");
        yield return new WaitForSeconds(lowGStartTimer);
        _rb.gravityScale = _lowGravityScale;

        yield return new WaitForSeconds(_lowGravityTimer);
        _rb.gravityScale = _normalGravityScale;

        yield return null;
    }

    private void HandleJump()
    {
        _jumpToConsume = true;
        if (_canJumpFromPlatform || _coyoteTimeJumpUsable) DoJump();
        else if (!_hasJustJumped) StartCoroutine(HandleJumpBuffer());
    }

    private void DoJump()
    {
        _rb.gravityScale = _normalGravityScale;



        _rb.AddForce(Vector2.up * _playerJumpForce, ForceMode2D.Impulse);
        _jumpToConsume = false;
        _hasJustJumped = true;

        StartCoroutine(HandleLowGravityAtJumpPeak());
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "Platform")
    //     {
    //         _hasJustJumped = false;
    //         _isGrounded = true;
    //     }
    // }

    // private void OnCollisionStay2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "Platform")
    //     {
    //         _hasJustJumped = false;
    //         _isGrounded = true;
    //     }
    // }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            // _isGrounded = false;

            if (!_hasJustJumped)
                StartCoroutine(HandleCoyoteTime());
            else _hasJustJumped = false;
        }
    }


    public void OnMove(InputAction.CallbackContext context) // fixed update -> put a bool that allow to know if player is moving or not
    {
        if (context.performed)
        {
            _playerDir = context.ReadValue<float>().Normalize();
        }
        else if (context.canceled)
        {
            _playerDir = 0;
        }
    }

    public void OnJump(InputAction.CallbackContext context) // make jump -> test if can jump -> is grounded (stays true for .15s after stepping out of a platform) + jump buffer
    {
        if (context.performed)
        {
            HandleJump();
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

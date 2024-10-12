using System.Collections;
using UnityEngine;

public class PlatformDetection : MonoBehaviour
{
    public bool _isGrounded = false;
    public bool _hasJustLeftPlatform = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _isGrounded = true;
            _hasJustLeftPlatform = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _isGrounded = true;
            _hasJustLeftPlatform = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _isGrounded = false;
            _hasJustLeftPlatform = true;
        }
    }
}

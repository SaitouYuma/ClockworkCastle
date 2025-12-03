using UnityEngine;

public class Playermove : MonoBehaviour
{
    [SerializeField]float _playerSpeed = 5f;
    [SerializeField] float _playerJump = 10f;
    private bool _isGrounded = false;
    Rigidbody2D _rb;
    private float x;
    void Start()
    {
       _rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    void Update()
    { 
        x = Input.GetAxis("Horizontal");
        _rb.linearVelocity = new Vector2(x * _playerSpeed, _rb.linearVelocity.y);
        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded==true)
        {
            _rb.linearVelocity = new Vector2(x * _playerSpeed, _playerJump);
        }
    }
}

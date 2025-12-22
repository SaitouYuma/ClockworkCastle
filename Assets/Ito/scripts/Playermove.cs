using Unity.VisualScripting;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    Transform currentGround;
    [SerializeField] float rayLength = 0.7f;
    private Vector2 _rayPos;
    [SerializeField] GameObject _player;
    [SerializeField] float _playerSpeed = 5f;
    [SerializeField] float _playerJump = 10f;
    [SerializeField] int _playerHp = 1;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D _rb;
    private float x;
    DeviceController device;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
   
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        return hit.collider != null;
    }
    void Update()
    {
        CheckGround();
        x = Input.GetAxis("Horizontal");
        _rb.linearVelocity = new Vector2(x * _playerSpeed, _rb.linearVelocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            _rb.linearVelocity = new Vector2(x * _playerSpeed, _playerJump);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ItemPickup();
        }
        if(device != null && Input.GetKeyDown(KeyCode.F))
        { 
            device.Activate();
        }

    }
    public void TakeDamage(int damage)
    {
        _playerHp -= damage;
        if (_playerHp <= 0)
        {
            Dead();
        }
    }
    void CheckGround()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.down * 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            rayLength,
            groundLayer
        );

        if (hit.collider != null && hit.collider.CompareTag("Wheelground"))
        {
            if (currentGround != hit.transform)
            {
                transform.SetParent(hit.transform);
                currentGround = hit.transform;
            }
        }
        else
        {
            if (currentGround != null)
            {
                transform.SetParent(null);
                currentGround = null;
            }
        }
    }

    void ItemPickup()
    {
        //ƒAƒCƒeƒ€ˆ—
    }
    void Dead()
    {
        Debug.Log("player‚ªŽ€‚ñ‚¶‚á‚Á‚½");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Device"))
        {
            device = other.GetComponent<DeviceController>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Device"))
        {
            device = null;
        }
    }
}

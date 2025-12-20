using Unity.VisualScripting;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    private Vector2 _rayPos;
    [SerializeField] GameObject _player;
    [SerializeField] float _playerSpeed = 5f;
    [SerializeField] float _playerJump = 10f;
    [SerializeField] int _playerHp = 1;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D _rb;
    private float x;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        return hit.collider != null;
    }
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        _rb.linearVelocity = new Vector2(x * _playerSpeed, _rb.linearVelocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            
            _rb.linearVelocity = new Vector2(x * _playerSpeed, _playerJump);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            ItemPickup();
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
          
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
    void ItemPickup()
    {
        //ƒAƒCƒeƒ€ˆ—
    }
    void Dead()
    {
        Debug.Log("player‚ªŽ€‚ñ‚¶‚á‚Á‚½");
    }

}

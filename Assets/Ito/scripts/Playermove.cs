using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Playermove : MonoBehaviour
{
    Transform currentGround;
    [SerializeField] float rayLength = 0.7f;
    [SerializeField] float _playerSpeed = 5f;
    [SerializeField] float _playerJump = 10f;
    [SerializeField] int _playerHp = 1;
    [SerializeField] LayerMask groundLayer;
    Animator _anim;
    Rigidbody2D _rb;
    private float x;
    float _yvelo;
    DeviceController device;
    bool _grounded;
    bool _isDead;
    Vector2 _origin;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    bool IsGrounded()
    {
        _origin = (Vector2)transform.position + Vector2.down * 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(
            _origin,
            Vector2.down,
            rayLength,
            groundLayer
        );
        return hit.collider != null;
    }

    void Update()
    {
        _yvelo = _rb.linearVelocity.y;
        CheckGround();
        x = Input.GetAxis("Horizontal");
        _rb.linearVelocity = new Vector2(x * _playerSpeed, _yvelo);
        _anim.SetFloat("Speed", Mathf.Abs(x));//走るアニメーションの処理
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            _rb.linearVelocity = new Vector2(x * _playerSpeed, _playerJump);
            _anim.SetTrigger("Jump");
        }

        _anim.SetFloat("Yvelocity", _yvelo);//落ちるアニメーションの処理

        _grounded = IsGrounded();
        _anim.SetBool("IsGrounded", _grounded);//アニメーションに使う接地判定のbool

        if(x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
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
        Vector2 origin = (Vector2)transform.position + Vector2.down * 1.5f;
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
        //アイテム処理
    }
    void Dead()
    {
        _isDead = true;
        _anim.SetBool("IsDead", _isDead);
    }

    public void OnDeadAnimationEnd()
    {
        GameManager.instance.PlayerDead();
        Destroy(gameObject);
        Debug.Log("お前はもう死んでいる");
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

using UnityEngine;
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
    Vector2 origin;
    Vector2 _direction;
    RaycastHit2D hit;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }



    void Update()
    {
        _yvelo = _rb.linearVelocity.y;
        IsGrounded();
        x = Input.GetAxis("Horizontal");
        _rb.linearVelocity = new Vector2(x * _playerSpeed, _yvelo);
        _anim.SetFloat("Speed", Mathf.Abs(x));//走るアニメーションの処理
        if (Input.GetKeyDown(KeyCode.Space) && _grounded == true)
        {
            if (GravitySwitch.Instance != null && GravitySwitch.Instance.IsGravityReversed)
            {
                // 重力反転中は下向きにジャンプ
                _rb.linearVelocity = new Vector2(x * _playerSpeed, _playerJump * -1);
                AudioManager.instance.PlaySE("jump");
            }
            else
            {
                // 通常は上向きにジャンプ
                _rb.linearVelocity = new Vector2(x * _playerSpeed, _playerJump);
                AudioManager.instance.PlaySE("jump");
            }
            _anim.SetTrigger("Jump");
        }

        _anim.SetFloat("Yvelocity", _yvelo);//落ちるアニメーションの処理

        _anim.SetBool("IsGrounded", _grounded);//アニメーションに使う接地判定のbool

        if (x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ItemPickup();
        }
        if (device != null && Input.GetKeyDown(KeyCode.F))
        {
            device.Activate();
        }

        // 親子関係がない時だけスケールを設定
        if (GravitySwitch.Instance == null) return;
        if (currentGround == null)
        {
            if (GravitySwitch.Instance.IsGravityReversed)
            {
                transform.localScale = new Vector3(4, -4, 1);
            }
            else
            {
                transform.localScale = new Vector3(4, 4, 1);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        _playerHp -= damage;
        if (_playerHp <= 0)
        {
            AudioManager.instance.PlaySE("Dead");
            Dead();
        }
    }
    void IsGrounded()
    {
        
        if (GravitySwitch.Instance == null) return;

        if (!GravitySwitch.Instance.IsGravityReversed)//反転してなかったら
        {
            origin = (Vector2)transform.position + Vector2.down * 1.7f;
            _direction = Vector2.down;
        }
        else//反転したら
        {
            origin = (Vector2)transform.position + Vector2.up * 1.7f;
            _direction = Vector2.up;
        }

        hit = Physics2D.Raycast(
            origin,
            _direction,
            rayLength,
            groundLayer
        );

        if (hit.collider != null && hit.collider.CompareTag("Wheelground"))
        {
            if (currentGround != hit.transform)
            {
                transform.SetParent(hit.transform, true);
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
        _grounded = hit.collider != null;
        Debug.DrawRay(origin, _direction * rayLength, Color.red);
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

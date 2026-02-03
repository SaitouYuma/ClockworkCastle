using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float _detectionrange = 10f;
    [SerializeField] private float _jumpRange = 2f;
    [SerializeField] private float _Jumpingpower = 300f;

    private Transform _Tr;
    private Rigidbody2D _Rb;
    private float distanceOfPlayer = 0f;
    private bool _isGrounded = false;

    private void Start()
    {
        _Tr = transform;
        _Rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                _target = player;
            }
            else
            {
                return; // プレイヤーが見つからない場合は何もしない
            }
        }

        // 上下を含めた実際の距離で感知
        distanceOfPlayer = Vector2.Distance(_target.transform.position, _Tr.position);

        if (distanceOfPlayer < _detectionrange)
        {
            // 移動は横方向のみ（Y座標は固定）
            Vector3 targetPos = new Vector3(_target.transform.position.x, _Tr.position.y, _Tr.position.z);
            _Tr.position = Vector3.MoveTowards(_Tr.position, targetPos, speed * Time.deltaTime);
        }

        if (distanceOfPlayer < _jumpRange && _isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _Rb.AddForce(Vector2.up * _Jumpingpower);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
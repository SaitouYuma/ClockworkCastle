using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float speed = 1.0f;
    private Transform _Tr;
    private Rigidbody2D _Rb;
    private float distanceOfPlayer = 0f;
    [SerializeField] private float _detectionrange = 10f;
    [SerializeField] private float _jumpRange = 2f;
    [SerializeField] private float _Jumpingpower = 300f;
    private bool _isGrounded = false;

    private void Start()
    {
        _Tr = transform;
        _Rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 targetPos = new Vector3(_target.transform.position.x, _Tr.position.y, _Tr.position.z);
        distanceOfPlayer = Vector3.Distance(targetPos, _Tr.position);
        if (distanceOfPlayer < _detectionrange)
        {
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

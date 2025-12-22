using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    enum State
    {
        Idle,
        Chase,
        Retreat,
        Charge,
        Dash,
        Cooldown,
        Decoy
    }
    [SerializeField] float activateDistance = 6f;
    [Header("Move")]
    [SerializeField] float chaseSpeed = 2f;
    [SerializeField] float retreatSpeed = 2.5f;
    [SerializeField] float dashSpeed = 7f;

    [Header("Distance")]
    [SerializeField] float dashStartDistance = 3f;
    [SerializeField] float retreatDistance = 5f;

    [Header("Time")]
    [SerializeField] float chargeTime = 0.6f;
    [SerializeField] float dashTime = 0.4f;
    [SerializeField] float cooldownTime = 1f;

    [Header("Decoy")]
    [SerializeField] float decoyDetectRange = 6f; // 囮に反応する距離
    [SerializeField] float decoySpeed = 10f;       // 囮に向かう速さ

    Transform decoyTarget;
    State previousState;

    Transform player;
    State state = State.Idle;
    float timer;
    Vector2 dashDir;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        CheckDecoy();

        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Retreat:
                Retreat();
                break;
            case State.Charge:
                Charge();
                break;
            case State.Dash:
                Dash();
                break;
            case State.Cooldown:
                Cooldown();
                break;
            case State.Decoy:
                Decoy();
                break;
        }
    }

    void Chase()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        Vector2 dir = (player.position - transform.position).normalized;
        transform.position += (Vector3)(dir * chaseSpeed * Time.deltaTime);

        if (dist < dashStartDistance)
        {
            state = State.Retreat;
        }
    }

    void Retreat()
    {
        Vector2 dir = (transform.position - player.position).normalized;
        transform.position += (Vector3)(dir * retreatSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            state = State.Charge;
            timer = 0f;
        }
    }

    void Charge()
    {
        timer += Time.deltaTime;

        // 突進方向をここで固定（重要）
        dashDir = (player.position - transform.position).normalized;

        if (timer >= chargeTime)
        {
            timer = 0f;
            state = State.Dash;
        }
    }

    void Dash()
    {
        timer += Time.deltaTime;
        transform.position += (Vector3)(dashDir * dashSpeed * Time.deltaTime);

        if (timer >= dashTime)
        {
            timer = 0f;
            state = State.Cooldown;
        }
    }

    void Cooldown()
    {
        timer += Time.deltaTime;

        if (timer >= cooldownTime)
        {
            timer = 0f;
            state = State.Chase;
        }
    }

    void Idle()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        // 一定距離に入ったら起動
        if (dist <= activateDistance)
        {
            state = State.Chase;
        }
    }

    void CheckDecoy()
    {
        if (state == State.Decoy) return;

        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            decoyDetectRange,
            LayerMask.GetMask("Decoy")
        );

        if (hit != null)
        {
            decoyTarget = hit.transform;
            previousState = state;
            state = State.Decoy;
        }
    }

    void Decoy()
    {
        if (decoyTarget == null)
        {
            state = previousState; // 囮が消えたら元に戻る
            return;
        }

        Vector2 dir = (decoyTarget.position - transform.position).normalized;
        transform.position += (Vector3)(dir * decoySpeed * Time.deltaTime);
    }

}

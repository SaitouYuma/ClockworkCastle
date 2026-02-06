using UnityEngine;
using System.Collections;

public class PressMachine : MonoBehaviour
{
    [SerializeField] float _downSpeed = 8f;
    [SerializeField] float _upSpeed = 3f;
    [SerializeField] float _waitTime = 1f;
    [SerializeField] float _pressDistance = 3f;

    Vector3 startPos;
    bool _isActive = false;

    void Start()
    {
        startPos = transform.position;
        StartCoroutine(PressRoutine());
    }

    IEnumerator PressRoutine()
    {
        while (true)
        {
            while (!_isActive)
                yield return null;
            // ‘Ò‹@
            yield return new WaitForSeconds(_waitTime);

            // ‰Ÿ‚µ’×‚·
            Vector3 downPos = startPos + Vector3.down * _pressDistance;
            AudioManager.instance.PlaySE("Press");
            while (Vector3.Distance(transform.position, downPos) > 0.01f)
            {
                if (!_isActive)
                    yield return null;
                transform.position = Vector3.MoveTowards(transform.position, downPos, _downSpeed * Time.deltaTime);

                yield return null;
            }

            // ­‚µ‘Ò‚Â
            yield return new WaitForSeconds(0.2f);

            // Œ³‚ÌˆÊ’u‚É–ß‚é
            while (Vector3.Distance(transform.position, startPos) > 0.01f)
            {
                if (!_isActive)
                    yield return null;
                transform.position = Vector3.MoveTowards(transform.position, startPos, _upSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }

    public void Toggle()
    {
        _isActive = !_isActive;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            AudioManager.instance.PlaySE("Press");
        }
    }
}

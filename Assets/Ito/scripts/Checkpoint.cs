using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    GameManager _gm;
    [SerializeField] SpriteRenderer _checkpoint;
    [SerializeField] SpriteRenderer _checkpointed;
    public bool _ischeck;

    void Start()
    {
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _checkpoint.gameObject.SetActive(true);
        _checkpointed.gameObject.SetActive(false);

        Debug.Log(transform.position);
     }
    void OnTriggerEnter2D(Collider2D col)
    {
      if(col.gameObject.CompareTag("Player"))
        {
            _gm._checkPointPos = transform.position;
            _checkpoint.gameObject.SetActive(false);
            _checkpointed.gameObject.SetActive(true);
            Debug.Log("チェックポインツ！");
            _ischeck = true;
        }
    }
}

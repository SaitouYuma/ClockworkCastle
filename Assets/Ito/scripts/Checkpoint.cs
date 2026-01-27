using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    GameManager _gm;
    [SerializeField] SpriteRenderer _flag;
    public bool _ischeck;

    void Start()
    {
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _flag.color = Color.blue;
     }
    void OnTriggerEnter2D(Collider2D col)
    {
      if(col.gameObject.CompareTag("Player"))
        {
            _gm._checkPointPos = transform.position;
            _flag.color = Color.white;
            Debug.Log("チェックポインツ！");
            _ischeck = true;
        }
    }
}

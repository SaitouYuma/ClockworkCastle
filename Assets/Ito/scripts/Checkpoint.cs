using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Checkpoint : MonoBehaviour
{
    GameManager _gm;
    [SerializeField] SpriteRenderer _flag;
    public bool _ischeck;
    Vector2 _respawnPos;

    void Start()
    {
        _flag.color = Color.blue;
        _respawnPos = transform.position;
     }
    void OnTriggerEnter2D(Collider2D col)
    {
      if(col.gameObject.CompareTag("Player"))
        {
            _respawnPos = new Vector2(transform.position.x,transform.position.y +1);
            GameManager.instance._checkPointPos = _respawnPos;
            _flag.color = Color.white;
            if(!_ischeck)
            {
                AudioManager.instance.PlaySE("flag");
                _ischeck = true;
            }
        }
    }
}

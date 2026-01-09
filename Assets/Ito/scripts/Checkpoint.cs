using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameManager _gm;

    void Start()
    {
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
      if(col.gameObject.CompareTag("Player"))
        {
            _gm._checkPointPos = transform.position;
            Debug.Log("チェックポインツ！");
        }
    }


}

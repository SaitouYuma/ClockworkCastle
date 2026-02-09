using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool _isnear;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            _isnear = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _isnear = false;
        }
    }
    void Update()
    {
        if(_isnear&&Input.GetKeyDown(KeyCode.F))
        {
            GameManager.instance.Goal();
        }
    }
}

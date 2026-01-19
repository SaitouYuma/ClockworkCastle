using UnityEngine;

public class test : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Playermove>().TakeDamage(1);
        }
    }
}

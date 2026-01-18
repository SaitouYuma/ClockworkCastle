using UnityEngine;

public class test : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collsion)
    {
        if (collsion.gameObject.CompareTag("Player"))
        {
            collsion.gameObject.GetComponent<Playermove>().TakeDamage(1);
        }
    }
}

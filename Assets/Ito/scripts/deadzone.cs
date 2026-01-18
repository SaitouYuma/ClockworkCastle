using UnityEngine;

public class deadzone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Playermove>().TakeDamage(1);
        }
    }
}

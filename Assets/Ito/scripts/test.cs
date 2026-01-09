using UnityEngine;

public class test : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("‚¢‚Á‚Ä‚¦‚¦‚¦‚¦");
            col.gameObject.GetComponent<Playermove>().TakeDamage(1);
        }
    }
}

using UnityEngine;

public class Decoy : MonoBehaviour
{
    [SerializeField] float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}

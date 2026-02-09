using UnityEngine;
using UnityEngine.UI;

public class Playerlife : MonoBehaviour
{
    [SerializeField] Image [] _lifeImages;

    void Start()
    {
        GameManager.instance.SetLifeImages(_lifeImages);
    }
}

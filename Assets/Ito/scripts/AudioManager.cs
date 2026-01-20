using NUnit.Framework;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SE;


    private void Awake()
    {
        // ƒVƒ“ƒOƒ‹ƒgƒ“‚Ìİ’è
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

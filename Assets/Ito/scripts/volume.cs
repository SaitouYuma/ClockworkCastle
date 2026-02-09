using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class volume : MonoBehaviour
{
    [SerializeField] AudioMixer _am;
    public void SetBGM(float value)
    {
        _am.SetFloat("BGMVolume", Mathf.Log10(value) * 20);
    }

    public void SetSE(float value)
    {
        _am.SetFloat("SEVolume", Mathf.Log10(value) * 20);
    }

}

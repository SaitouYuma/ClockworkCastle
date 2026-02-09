using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class volume : MonoBehaviour
{
    [SerializeField] AudioMixer _am;
    [SerializeField] float _lastSETime;
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _seSlider;
    bool _isInit = true; 
    float _bgm = 1f;
    float _se = 1f;
    public void SetBGM(float value)
    {
        _am.SetFloat("BGMVolume", Mathf.Log10(value) * 20);
    }

    public void SetSE(float value)
    {
        _am.SetFloat("SEVolume", Mathf.Log10(value) * 20);
        if (_isInit) return;
        if (Time.time - _lastSETime > 1f)
        {
            AudioManager.instance.PlaySE("UI");
            
            _lastSETime = Time.time;
        }
    }
    void Start()
    {
        _am.GetFloat("BGMVolume", out _bgm);
        _am.GetFloat("SEVolume", out _se);

        _bgmSlider.value = Mathf.Pow(10f, _bgm / 20f);
        _seSlider.value = Mathf.Pow(10f, _se / 20f);
        _isInit = false;
    }

}

using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("AudioSource")]
    public AudioSource _bgmSource;
    public AudioSource _seSource;

    [Header("BGM List")]
    public List<BGMData> _bgmList;

    [Header("SE List")]
    public List<SEData> _seList;

    Dictionary<string, BGMData> _bgmDict = new Dictionary<string, BGMData>();
    Dictionary<string, SEData> _seDict = new Dictionary<string, SEData>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (var data in _bgmList)//BGM‚ð“o˜^
        {
            if (data.clip != null)
                _bgmDict[data.key] = data;
        }

        foreach (var data in _seList)//SE‚ð“o˜^
        {
            if (data.clip != null)
                _seDict[data.key] = data;
        }
    }

    public void PlayBGM(string name)
    {
        if (!_bgmDict.ContainsKey(name))
            return;

        var data = _bgmDict[name];

        if (_bgmSource.clip == data.clip && _bgmSource.isPlaying)
            return;

        _bgmSource.clip = data.clip;
        _bgmSource.loop = data.loop;
        _bgmSource.volume = data.volume;
        _bgmSource.Play();
    }

    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    public void PlaySE(string name)
    {
        if (!_seDict.ContainsKey(name))
            return;

        var data = _seDict[name];
        _seSource.PlayOneShot(data.clip,data.volume);
    }

    public void SetBGMVolume(float volume)
    {
        _bgmSource.volume = volume;
    }

    public void SetSEVolume(float volume)
    {
        _seSource.volume = volume;
    }
}

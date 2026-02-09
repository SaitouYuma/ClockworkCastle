using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FadeManager : MonoBehaviour
{
    [SerializeField] float Speed = 0.005f;        //フェードするスピード
    float red, green, blue, alfa;

    public static FadeManager instance;

    [SerializeField] Image fadeImage;                //パネル


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; // ← これ超重要
        }

        instance = this;
        DontDestroyOnLoad(transform.root.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(Startt());
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void FadeOutAndLoad(string sceneName)
    {
        StartCoroutine(FadeOutCoroutine(sceneName));
    }

     IEnumerator FadeOutCoroutine(string sceneName)
    {
        alfa = 0f;
        fadeImage.enabled = true;
        while (alfa < 1)
        {
            alfa += Speed;
            Alpha();
            yield return null;
        }
        SceneManager.LoadScene(sceneName);

        while (alfa > 0)
        {
            alfa -= Speed;
            Alpha();
            yield return null;
        }

        fadeImage.enabled = false;
    }

    void Alpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
    IEnumerator Startt()
    {
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;

        alfa = 1f;
        fadeImage.enabled = true;
        Alpha();

        while (alfa > 0)
        {
            alfa -= Speed;
            Alpha();
            yield return null;
        }

        fadeImage.enabled = false;
    }

}

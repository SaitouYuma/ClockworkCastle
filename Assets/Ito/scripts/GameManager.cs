using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int _playerlife = 3;

    private void Awake()
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
    }
    public enum GameState
    {
        Title,
        Playing,
        Paused,
        Result,
        GameOver
    }
   
    public void SceneChanger(int number)
    {
        SceneManager.LoadScene(number);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif  
    }
    public void Gameover()
    {
        SceneChanger(0);//ゲームオーバーシーンへ
    }
}

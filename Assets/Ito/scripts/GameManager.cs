using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int _playerstock = 3;

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
    public void PlayerDead()
    {
        _playerstock--;
        Debug.Log("残りストックは" + _playerstock);
        if (_playerstock > 0)
        {
            Respawn();
        }
        else
        {
            Gameover();
        }
    }
    public void Respawn()
    {

    }

    public void Gameover()
    {
        SceneChanger(0);//ゲームオーバーシーンへ
    }
}

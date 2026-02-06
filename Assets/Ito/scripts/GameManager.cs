using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _playerPrefab;
    GameObject _currentplayer;
    public static GameManager instance;
    [SerializeField] int _playerstock = 3;
    public Vector2 _checkPointPos;
    [SerializeField] Image [] _playerlifeImage;

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
    private void Start()
    {
        AudioManager.instance.PlayBGM(SceneManager.GetActiveScene().name);
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
        Hpupdate();
        if (_playerstock > 0)
        {
            Respawn();
            if (GravitySwitch.Instance != null && GravitySwitch.Instance.IsGravityReversed)
            {
                GravitySwitch.Instance.SetOn(false);
            }
        }
        else
        {
            Gameover();
        }
    }
    public void Respawn()
    {
        Debug.Log(_checkPointPos);

        _currentplayer = Instantiate(_playerPrefab, _checkPointPos, Quaternion.identity);

        cameraaa cam = Camera.main.GetComponent<cameraaa>();
        cam.Settarget(_currentplayer);
    }
    void Hpupdate()
    {
        for(int i = 0;i<_playerlifeImage.Length;i++)
        {
            if (i < _playerstock)
            {
                _playerlifeImage[i].enabled = true;   // 表示
            }
            else
            {
                _playerlifeImage[i].enabled = false;  // 非表示
            }
        }
    }

    public void Gameover()
    {
        SceneChanger(3);//ゲームオーバーシーンへ
        Debug.Log("げーむおーばー");
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioManager.instance.PlayBGM(scene.name);
    }
}

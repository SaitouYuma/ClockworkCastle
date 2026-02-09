using System.Collections;
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
    Image [] _playerlifeImage;

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
            FadeManager.instance.FadeOutAndLoad("Rizaruto2");
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
        if (_playerlifeImage == null) return;
        for (int i = 0;i<_playerlifeImage.Length;i++)
        {
            if (i < _playerstock)
            {
                _playerlifeImage[i].enabled = true;   // •\Ž¦
            }
            else
            {
                _playerlifeImage[i].enabled = false;  // ”ñ•\Ž¦
            }
        }
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
        if (scene.name == "IngameScene")
        {
            _playerstock = 3;
            _checkPointPos = new Vector2(-4,0);
            Hpupdate();
        }
        else
        {
            _playerlifeImage = null;
        }
    }
   public void Goal()
    {
        FadeManager.instance.FadeOutAndLoad("Rizaruto");
    }
    public void SetLifeImages(Image[] images)
    {
        _playerlifeImage = images;
        Hpupdate();
    }

}

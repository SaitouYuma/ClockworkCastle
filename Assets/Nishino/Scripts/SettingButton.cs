using UnityEngine;
using UnityEngine.SceneManagement;
public class SettingButton : MonoBehaviour 
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Setting");
    }
}

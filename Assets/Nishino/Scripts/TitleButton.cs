using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleButton : MonoBehaviour
{
    public void StartBtn()
    {
        FadeManager.instance.FadeOutAndLoad("IngameScene");
    }
}

using UnityEngine;

public class GameClearOverUI : MonoBehaviour
{
    public void OnTitleButtonClick()
    {
        if (GameManager.instance != null)
        {
            FadeManager.instance.FadeOutAndLoad("Title");
        }
    }

    public void OnRestartButtonClick()
    {
        if (GameManager.instance != null)
        {
            FadeManager.instance.FadeOutAndLoad("IngameScene");
        }
    }
}

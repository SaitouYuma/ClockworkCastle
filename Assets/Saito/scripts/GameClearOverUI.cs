using UnityEngine;

public class GameClearOverUI : MonoBehaviour
{
    public void OnTitleButtonClick()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SceneChanger(0);
        }
    }

    public void OnRestartButtonClick()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SceneChanger(1);
        }
    }
}

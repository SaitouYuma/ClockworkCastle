using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void OnQuitButtonClick()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.QuitGame();
        }
    }
}

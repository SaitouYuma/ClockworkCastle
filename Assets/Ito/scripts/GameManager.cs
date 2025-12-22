using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void SceneChanger(int number)
    {
        SceneManager.LoadScene(number);
    }
}

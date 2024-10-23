using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    bool isPaused;
    
    void Start()
    {
        isPaused = false;
    }
    
    public void PauseGame()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Reiniciar ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

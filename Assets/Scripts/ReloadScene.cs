using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

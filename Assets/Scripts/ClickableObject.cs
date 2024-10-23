using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickableObject : MonoBehaviour
{
    //Audio
    //Audio
    public AudioClip mineralDest;
    public AudioClip clickMineral;
    private AudioSource audio_S;

    public int clicksToDestroy = 3; // N�mero de clics necesarios para destruir el objeto
    private int currentClicks = 0;
    private SpawnObject spawnManager;
    public Transform player;
    public float range;

    private void Start()
    {
        audio_S = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").transform;
    }
    // M�todo para recibir la referencia del SpawnObject
    public void SetSpawnManager(SpawnObject manager)
    {
        spawnManager = manager;
    }

    private void OnMouseDown()
    {
        range = Vector2.Distance(transform.position, player.position);
        if (range < 1f )
        {
            currentClicks++;

            if (currentClicks >= clicksToDestroy)
            {
                audio_S.PlayOneShot(mineralDest, 1);
                Destroy(gameObject);
                player.GetComponent<Player>().puntos += 1 * Random.Range(10,100);
                GameObject.FindWithTag("Points").GetComponent<Text>().text = "" + player.GetComponent<Player>().puntos;
 
                // Llamar al SpawnManager para generar otro objeto
                if (spawnManager != null)
                {
                spawnManager.SpawnObj();
                }
            }
        }
       
    }
}

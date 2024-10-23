
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System;

public class Player : MonoBehaviour
{
       
    //Variables Timer
    private int minutos, segundos;
    public float timer;
    [SerializeField]TextMeshProUGUI textTimer;
    bool tiempoFin;

    //puntaje
    public float puntos;

    //movimiento y velocidad
    public float movSpeed;
    private float speedX, speedY;
    public Rigidbody2D rb;

    //Vida del jugador
    public HealthBar healthBar;

    //Referencia texto muerte
    public GameObject deathMessage;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();

        // Buscar el componente HealthBar

        if (healthBar == null)
        {
            healthBar = FindObjectOfType<HealthBar>();
        }

        if (deathMessage != null)
        {
            deathMessage.SetActive(false);
        }
    }
 
    //Timer
    void Cronometro()
    {
        if (!tiempoFin)
            {
                timer -= Time.deltaTime;
            }
        minutos = Mathf.FloorToInt(timer/60);
        segundos = Mathf.FloorToInt(timer%60);
        textTimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        if (timer <=0)
            {
                tiempoFin = true;
                timer=0;
                deathMessage.SetActive(true);
                Time.timeScale = 0;
            }
    }
    void Update()
    {
        //Timer
        Cronometro();

        //Movimiento jugador
        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.velocity = new Vector2(speedX, speedY);

        // Verificar si la vida del jugador es 0 o menos
        if (healthBar.health <= 0)
        {
            Die();
        }

        // Direccion jugador
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.localRotation = quaternion.Euler (0f, 0f, 0f);
        }
   
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.localRotation = quaternion.Euler (0f, -60f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si colisiona con un objeto que tenga el tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Colisión con enemigo detectada!");  // Mensaje en la consola
            healthBar.TakeDamage(10); // Llamar a la función para reducir la salud
        }
    }

    void Die()
    {
        // Desactivar al jugador
        gameObject.SetActive(false);

        // Mostrar el mensaje de muerte
        if (deathMessage != null)
        {
            deathMessage.SetActive(true);
        }
    }
}

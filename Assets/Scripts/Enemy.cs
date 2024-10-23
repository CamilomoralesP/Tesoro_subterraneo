using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float enemySpeed;

    public int collisionCount = 0; //contador colisiones
    public int maxCollisions = 2; // # colisiones antes de que el enemigo muera
    public float despawnTime = 10f;

    private float distancePlayer;
    private Rigidbody2D rb;
    private float timer;
    
    private EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Iniciar el timer en 0 segundos
        timer = 0f;

        spawner = FindObjectOfType<EnemySpawner>(); // Obtener referencia al spawner
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player no asignado. Verifica que el Player sea asignado al enemigo.");
            return; // Si no hay Player asignado, no ejecuta el código restante
        }
        
        // Calcular la distancia entre el enemigo y el jugador
        distancePlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distancePlayer > 0.5f){
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * enemySpeed;

            // Incrementar el temporizador si no hay colisiones
            timer += Time.deltaTime; // Sumar el tiempo desde la última actualización

        } else {
            rb.velocity = Vector2.zero;
        }

        // Destruir al enemigo si el temporizador alcanza el tiempo de despaun
        if (timer >= despawnTime)
        {
            Debug.Log("El enemigo se ha desaparecido después de 10 segundos sin tocar al jugador.");
            Destroy(gameObject); // Destruir el enemigo
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player")){
            collisionCount++;
            Debug.Log("Collision con el jugador. Contador: " + collisionCount);

            // Reiniciar el temporizador al colisionar con el jugador
            timer = 0f; 

            //Destruir el enemigo despues de dos toques
            if (collisionCount >= maxCollisions){
                Debug.Log("El enemigo ha muerto.");
                Destroy(gameObject); 
            }
        }
    }

    void OnDestroy()
    {
        // Cuando el enemigo es destruido, notificar al spawner
        spawner.OnEnemyDestroyed();
    }
}

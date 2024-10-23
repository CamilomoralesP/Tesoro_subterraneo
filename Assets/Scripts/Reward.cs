using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public int rewardCount = 0; // Contador de colisiones
    public int maxRewards = 1;   // Número de colisiones para obtener la recompensa

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rewardCount++; // Incrementar el contador de colisiones
            Debug.Log("Colisión con el jugador. Contador de recompensas: " + rewardCount);

            if (rewardCount >= maxRewards)
            {
                GiveReward(other.gameObject); // Llama al método para dar la recompensa
                Destroy(gameObject); // Destruir el objeto de recompensa
            }
        }
    }

    void GiveReward(GameObject player)
    {
        Debug.Log("¡Recompensa otorgada al jugador!");

        // Ejemplo: Si tienes un script en el jugador para aumentar salud
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.healthBar.health += 10; // Aumentar la salud en 10 (ajusta según sea necesario)
        }
    }
}

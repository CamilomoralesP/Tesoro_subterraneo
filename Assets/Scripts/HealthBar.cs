using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    public float health;

    public Gradient gradient;
    public Image fill;

    void Start () {
        health = maxHealth;
        UpdateHealthBar();
    }

    // Metodo para recibir daño
    public void TakeDamage(float damage){
        health -= damage;
        Debug.Log("Daño recibido: " + damage + ". Salud restante: " + health);
        health = Mathf.Clamp(health, 0, maxHealth); // Asegurarse de que la salud no sea negativa
        UpdateHealthBar();
    }

    // Actualiza la barra de vida visualmente
    void UpdateHealthBar() {
        if (healthSlider != null) {
            healthSlider.value = health;
        }
        if (fill != null) {
            fill.color = gradient.Evaluate(health / maxHealth); // Cambiar color segun la salud
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    private float MinX, MaxX, MinY, MaxY;
    private Vector2 pos;
    public GameObject[] myGameObjectToRespawn;

    // Nuevas variables para definir el área de aparición
    public float spawnAreaMinX = -8.5f;
    public float spawnAreaMaxX = 8.5f;
    public float spawnAreaMinY = -4.5f;
    public float spawnAreaMaxY = 4.5f;

    // Tamaño del radio para verificar colisiones al intentar colocar un nuevo objeto
    public float checkRadius = 0.5f;

    private void Start()
    {
        SpawnObj();
    }

    public void SpawnObj()
    {
        int NumberOfObj = Random.Range(0, myGameObjectToRespawn.Length);

        // Intentar encontrar una posición libre hasta 10 veces
        bool foundPosition = false;
        int attempts = 0;
        while (!foundPosition && attempts < 10)
        {
            pos = new Vector2(Random.Range(spawnAreaMinX, spawnAreaMaxX), Random.Range(spawnAreaMinY, spawnAreaMaxY));

            // Verificar si la posición generada está cerca del Grid usando la etiqueta
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, checkRadius);
            bool isNearGrid = false;
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("obstacle"))
                {
                    isNearGrid = true;
                    break;
                }
            }

            if (!isNearGrid)
            {
                foundPosition = true;
            }

            attempts++;
        }

        // Solo instanciar si se encontró una posición libre
        if (foundPosition)
        {
            GameObject obj = Instantiate(myGameObjectToRespawn[NumberOfObj], pos, Quaternion.identity);
            obj.transform.parent = transform;

            // Asignar el SpawnObject como manager del ClickableObject
            ClickableObject clickable = obj.GetComponent<ClickableObject>();
            if (clickable != null)
            {
                clickable.SetSpawnManager(this);
            }
        }
        else
        {
            Debug.LogWarning("No se pudo encontrar una posición libre para el objeto.");
        }
    }
}
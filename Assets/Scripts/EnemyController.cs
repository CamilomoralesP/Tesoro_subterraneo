using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    [SerializeField] private Transform[] points;
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private float timeEnemy;
    private float tiempoSiguienteEnemigos;
    void Start()
    {
        maxX = points.Max(point => point.position.x);
        minX = points.Min(point => point.position.x);
        maxY = points.Max(point => point.position.y);
        minY = points.Min(point => point.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        tiempoSiguienteEnemigos += Time.deltaTime;

        if (tiempoSiguienteEnemigos >= timeEnemy){
            tiempoSiguienteEnemigos = 0;
            MakeEnemy();
        }
    }

    private void MakeEnemy(){
        int numberEnemy = Random.Range(0,enemys.Length);
        Vector2 positionRandom = new Vector2(Random.Range (minX, maxX), Random.Range (minY, maxY));

        Instantiate(enemys[numberEnemy], positionRandom, Quaternion.identity);
    }
}

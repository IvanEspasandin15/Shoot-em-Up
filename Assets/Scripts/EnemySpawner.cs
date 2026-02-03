using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] float interval;  // Intervalo entre Generaciones de Naves
    [SerializeField] float delay;  // Tiempo de espera para comenzar a generar Naves
    [SerializeField] GameObject enemy;  // Prefab de las Naves
    const float MIN_X = -3.5f;  // Coordenada Mínima en X
    const float MAX_X = 3.5f;  // Coordenada Máxima en X

    // Se ejecuta cuando se inicia el Script, antes del Primer Frame
    void Start() {
        StartCoroutine("EnemySpawn");  // Inicia la Co-Rutina de Spawn de Naves
    }

    // Generar los Asteroides
    IEnumerator EnemySpawn() {
        yield return new WaitForSeconds(delay);  // Pausa antes de empezar a generar las Naves

        // Generación Infinita de Naves
        while(true) {
            Vector3 position = new Vector3(Random.Range(MIN_X, MAX_X), transform.position.y, 0);  // Generar una Posición Aleatoria en X
            Instantiate(enemy, position, Quaternion.identity);  // Instanciar una Nave
            yield return new WaitForSeconds(interval);  // Esperar antes de generar la siguiente Nave
        }
    }
}
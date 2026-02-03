using UnityEngine;
using System.Collections;

public class AsteroidsSpawner : MonoBehaviour {
    [SerializeField] float interval;  // Intervalo entre Generaciones de Asteroides
    [SerializeField] float delay;  // Tiempo de espera para comenzar a generar Asteroides
    [SerializeField] GameObject AsteroidBig;  // Prefab de los Asteroides
    const float MIN_X = -2.5f;  // Coordenada Mínima en X
    const float MAX_X = 2.5f;  // Coordenada Máxima en X

    // Se ejecuta cuando se inicia el Script, antes del Primer Frame
    void Start() {
        StartCoroutine("AsteroidSpawn");  // Inicia la Co-Rutina de Spawn de Asteroides
    }

    // Generar los Asteroides
    IEnumerator AsteroidSpawn() {
        yield return new WaitForSeconds(delay);  // Pausa antes de empezar a generar los Asteroides
        
        // Generación Infinita de Asteroides
        while(true) {
            Vector3 position = new Vector3(Random.Range(MIN_X, MAX_X), transform.position.y, 0);  // Generar una Posición Aleatoria en X
            Instantiate(AsteroidBig, position, Quaternion.identity);  // Instanciar un Asteroide
            yield return new WaitForSeconds(interval);  // Esperar antes de generar el siguiente Asteroide
        }
    }
}
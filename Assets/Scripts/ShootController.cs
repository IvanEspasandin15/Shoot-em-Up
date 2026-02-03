using UnityEngine;

public class ShootController : MonoBehaviour {
    [SerializeField] float speed;  // Velocidad del Disparo
    [SerializeField] float lifetime;  // Tiempo que duran los Disparos
    [SerializeField] GameObject hit;  // Prefab del Impacto

    // Se ejecuta cuando se inicia el Script, antes del Primer Frame
    void Start() {
        Destroy(gameObject, lifetime);  // Destruir el Disparo después de su Tiempo de Vida
    }

    // Se ejecuta una vez por Frame
    void Update() {
        transform.Translate(Vector3.up * speed * Time.deltaTime);  // Movimiento hacia arriba del Disparo
    }

    // Se ejecuta cuando el Objeto sale de la parte Visible de la Pantalla
    void OnBecameInvisible() {
        Destroy(gameObject);  // Destruir Disparo
    }

    // Se ejecuta cuando un Collider configurado como Trigger detecta otro Collider
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {  // Si el Impacto es contra un Enemigo
            Instantiate(hit, transform.position, Quaternion.identity);  // Crea el Impacto
            Destroy(other.gameObject);  // Destruye el Enemigo
            Destroy(gameObject);  // Destruye el Disparo

        }else if (other.CompareTag("AsteroidBig")) {  // Si el Impacto es contra un Asteroide
            Instantiate(hit, transform.position, Quaternion.identity);  // Crea el Impacto
            Destroy(gameObject);  // Destruye el Disparo
        }
    }
}
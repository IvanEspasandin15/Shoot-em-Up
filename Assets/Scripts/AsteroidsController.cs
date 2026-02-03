using UnityEngine;

public class AsteroidsController : MonoBehaviour {
    // Caída Vertical
    [SerializeField] float minSpeedY;
    [SerializeField] float maxSpeedY;

    // Movimiento Horizontal
    [SerializeField] float minSpeedX;
    [SerializeField] float maxSpeedX;

    [SerializeField] GameObject explosionPrefab;  // Prefab de las Explosiones
    Rigidbody2D rb;  // RigidBody del Asteroide
    const float DESTROY_Y = -5.5f;  // Altura a la que se Destruye el Asteroide

    // Se ejecuta cuando se inicia el Script, antes del Primer Frame
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        float speedY = Random.Range(minSpeedY, maxSpeedY);  // Velocidad Aleatoria en Y.
        float speedX = Random.Range(minSpeedX, maxSpeedX);  // Velocidad Aleatoria en X.
        rb.linearVelocity = new Vector2(speedX, -speedY);  // Asignar Velocidad.
    }

    // Se ejecuta una vez por Frame
    void Update() {
        if (transform.position.y < DESTROY_Y) {  // Destruír el Asteroide cuando llegue a la Altura Límite
            Destroy(gameObject);
        }
    }

    // Se ejecuta cuando dos Colliders comienzan a tocarse (Es necesario que uno de los dos tenga un RigidBody2D)
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) {  // Si el Asteroide toca al Jugador o a un Enemigo Explota
            Explode();
        }
    }

    // Destruír el Asteroide
    void Explode() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);  // Inicia la Animación de Explosión
        Destroy(gameObject);  // Destruye el Asteroide
    }
}
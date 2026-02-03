using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] float speed;  // Velocidad de la Nave
    const float DESTROY_Y = -5.5f;  // Altura a la que se destruye la Nave
    [SerializeField] GameObject explosionPrefab;  // Prefab de las Explosiones

    // Se ejecuta una vez por Frame
    void Update() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);  // Movimiento de la Nave
        if(transform.position.y < DESTROY_Y) {  // Destruír la Nave cuando llegue a la Altura Límite
            Destroy(gameObject);
            GameManager.GetInstance().UpdateScore(-50);
        }
    }

    // Se ejecuta cuando un Collider configurado como Trigger detecta otro Collider
    private void OnTriggerEnter2D(Collider2D other) {
        DestroyEnemy();  // Destruir la Nave al entrar en contacto con algo
        GameManager.GetInstance().UpdateScore(25);
    }

    // Destruir la Nave
    void DestroyEnemy() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);  // Inicia la Animación de Explosión
        Destroy(gameObject);  // Destruye la Nave
    }

    // Se ejecuta cuando dos Colliders comienzan a tocarse (Es necesario que uno de los dos tenga un RigidBody2D)
    private void OnCollisionEnter2D(Collision2D other) {
        DestroyEnemy();
    }
}
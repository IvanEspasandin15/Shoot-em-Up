using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {
    [SerializeField] private float force = 5f;  // Fuerza del Movimiento
    [SerializeField] private Vector3 endPosition;  // Posición Final de la Nave al Inicio
    [SerializeField] private float duration;  // Duración de la Transición de Inicio
    [SerializeField] int blinkNum;  // Número de Parpadeos en la Transición de Inicio
    [SerializeField] GameObject shootPrefab; // Prefab del Disparo
    [SerializeField] float shootOffset = 0.5f; // Distancia desde el centro de la Nave hasta la posición donde se creará el Disparo
    [SerializeField] GameObject explosion;  // Prefab de la Explosión
    private bool active = false;  // Controla si se puede realizar alguna Acción o no
    private Rigidbody2D rb;  // RigidBody de la Nave
    Vector3 initialPosition;  // Posición Inicial de la Nave
    GameManager gameManager;  // Instancia del GameManager

    // Se ejecuta cuando se inicia el Script, antes del Primer Frame
    void Start() {
        // Inicializar Variables
        gameManager = GameManager.GetInstance();
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("StartPlayer");
    }

    // Se ejecuta una vez por Frame
    void Update() {
        if (active && Input.GetKeyDown(KeyCode.Space)) {  // Si la nave puede realizar Acciones y se ha pulsado la tecla de Disparo
            Vector3 shootPosition = transform.position + Vector3.up * shootOffset;  // Posición en la que se instanciará el Disparo
            Instantiate(shootPrefab, shootPosition, Quaternion.identity);  // Crear el Disparo sin rotación en la Posición Correspondiente
        }
    }

    private void FixedUpdate() {
        if (active) {
            CheckMove();
        }
    }

    private void CheckMove() {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));  // Obtener la Dirección en ambos Ejes
        direction.Normalize();  // Normalizar el Vector para que tenga Magnitud 1
        rb.AddForce(direction * force, ForceMode2D.Impulse);  // Aplicar una Fuerza en la Dirección Obtenida
    }

    // Animación de Aparición de la Nave
    IEnumerator StartPlayer() {
        Material mat = GetComponent<SpriteRenderer>().material;  // Material de la Nave
        Color color = mat.color;  // Color del Material de la Nave
        Collider2D collider = GetComponent<Collider2D>();  // Collider de la Nave
        Vector3 initialPosition = transform.position;  // Posición Inicial
        float t = 0, t2 = 0;  // Tiempo que transcurre en cada uno de los Intervalos

        collider.enabled = false;  // Desactivar Colisiones de la Nave

        // Cada vez calculamos una nueva Posición que está entre la Inicial y la Final
        while (t < duration) {
            t += Time.deltaTime;  // Incrementar el Tiempo Transcurrido
            Vector3 newPosition = Vector3.Lerp(initialPosition, endPosition, t/duration);  // Interpolar la posición entre Inicial y Final según el Progreso
            transform.position = newPosition;  // Asignar a la Nave la nueva Posición

            t2 += Time.deltaTime;  // Incrementar el Temporizador del Parpadeo
            float newAlpha = blinkNum * (t2/duration);  // Calcular nuevo Alpha según el Progreso
            if (newAlpha > 1) {
                t2 = 0;  // Si el Alpha supera 1 se Reinicia el Temporizador para crear el Parpadeo
            }
            color.a = newAlpha;  // Asignar a la Nave la Transparencia Calculada
            mat.color = color;  // Aplicar el color al Material

            yield return null;  // Esperar al Siguiente Frame
        }

        // Reactivar Colisiones, Acciones y Color
        color.a = 1;
        mat.color = color;
        collider.enabled = true;
        active = true;
    }

    // Se ejecuta cuando dos Colliders comienzan a tocarse (Es necesario que uno de los dos tenga un RigidBody2D)
    private void OnCollisionEnter2D(Collision2D other) {
        string tag = other.gameObject.tag;
        if (tag == "Enemy" || tag == "AsteroidBig") {  // Si la nave choca con un Enemigo o Asteroide
            DestroyShip();
        }
    }

    // Destruir Nave
    void DestroyShip() {
        gameManager.LoseLife();  // Quitar una Vida
        gameManager.ResetScore();  // Resetear Puntuación
        
        // Si quedan Vidas
        if (gameManager.Lives > 0) {
            active = false;  // Bloquear Acciones de la Nave
            Instantiate(explosion, transform.position, Quaternion.identity);  // Crear Explosión
            transform.position = initialPosition;  // Mover la nave a la Posición Inicial
            StartCoroutine("StartPlayer");  // Iniciar Animación de Reaparición

        }else {
            // Fin de Juego
            Instantiate(explosion, transform.position, Quaternion.identity);  // Crear Explosión
            Destroy(gameObject);  // Destruir Nave
        }
    }
}
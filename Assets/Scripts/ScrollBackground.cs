using UnityEngine;

public class ScrollBackground : MonoBehaviour {
    [SerializeField] float speed;  // Velocidad de Movimiento del Fondo
    float height;  // Altura de la Imagen para pasarla a la parte Superior
    
    // Se ejecuta cuando se inicia el Script, antes del Primer Frame
    void Start() {
        height = GetComponent<SpriteRenderer>().bounds.size.y;  // Acceder a la Altura del SpriteRenderer
    }

    // Se ejecuta una vez por Frame
    void Update() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);  // En qué dirección y cuánto se mueve

        // Reposicionamos cuando el centro de la imagen haya recorrido toda su altura
        if (transform.position.y <- height) {  // Si el centro de la imagen ha recorrido toda su altura
            transform.Translate(Vector3.up * 2 * height);  // Desplazamos el doble de la altura para saltar a encima de la imagen que va hacia abajo
        }
    }
}
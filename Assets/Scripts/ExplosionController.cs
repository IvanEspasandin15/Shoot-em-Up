using UnityEngine;

public class ExplosionController : MonoBehaviour {
    const float DELAY = 0.25f;  // Tiempo de espera para Destruír el objeto de la Explosión
    [SerializeField] AudioClip explosionSound;  // Sonido de la Explosión

    // Se ejecuta cuando se inicia el Script, antes del Primer Frame
    void Start() {
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);  // Reproducir sonido de la Explosión en la posición de la Cámara
        Destroy(gameObject, DELAY);  // Destruir la Explosión con el debido Tiempo de Espera
    }
}
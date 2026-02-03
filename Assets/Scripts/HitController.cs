using UnityEngine;

public class HitController : MonoBehaviour {
    const float DELAY = 0.25f;  // Tiempo de espera para Destruír el objeto del Impacto
    [SerializeField] AudioClip clip;  // Sonido del Impacto

    // Se ejecuta cuando se inicia el Script, antes del Primer Frame
    void Start() {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);  // Reproducir el Sonido
        Destroy(gameObject, DELAY);  // Destruir el Impacto con el debido Tiempo de Espera
    }
}
using UnityEngine;

public class ScrollBackgroundShader : MonoBehaviour {
    [SerializeField] float speed;  // Velocidad de Movimiento del Fondo
    Renderer render;  // Renderizador del Objeto
    
    // Se ejecuta cuando se inicia el Script, antes del Primer Frame
    void Start() {
        render = GetComponent<Renderer>();
    }

    // Se ejecuta una vez por Frame
    void Update() {
        Vector2 offset = Vector2.up * speed * Time.time;  // Cantidad de Desplazamiento de la Textura
        render.material.mainTextureOffset = offset;  // Acceder al Material del Objeto y aplicar Desplazamiento
    }
}
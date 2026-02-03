using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    static GameManager instance;  // Instancia de esta Clase

    const int LIVES = 4;  // Número máximo de Apariciones de la nave
    [SerializeField] TextMeshProUGUI txtMaxScore;  // Texto de Puntuación Máxima
    [SerializeField] TextMeshProUGUI txtScore;  // Texto de Puntuación
    [SerializeField] TextMeshProUGUI txtMessage;  // Texto de Game Over
    [SerializeField] GameObject[] imgLives;  // Array para las imágenes de las Vidas
    int score;  // Puntuación
    int maxScore;  // Puntuación Máxima
    int lives = LIVES;  // Variable para las Vidas
    bool lifeAdded = false;  // Bandera de Vida Extra Añadida

    // Acceso de solo lectura para las vidas desde el exterior
    public int Lives {
        get { return lives; }
    }

    // Método estático para obtener la instancia del GameManager
    public static GameManager GetInstance() {
        return instance;
    }

    // Se ejecuta cuando se instancia el Script
    void Awake() {
        if(instance == null) {
            instance = this;    
            DontDestroyOnLoad(gameObject);  // Evitar que el Objeto se destruya al cambiar de Escena
        }else if(instance != this) {
            Destroy(gameObject);  // Si ya existe una instancia, destruimos el nuevo GameManager para mantener la Singularidad
        }
    }

    // Se ejecuta cuando se inicia el Script, antes del Primer Frame
    void Start() {
        txtMessage.gameObject.SetActive(false);
    }

    // Se ejecuta una vez por Frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {  // Si se pulsa la Tecla Escape
            Application.Quit();  // Salir del Juego Compilado

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // Salir del Juego en el Editor Unity
            #endif
        }

        if ((score == 2000) && (lives < 4) && !lifeAdded) {  // Si se consiguen 2000 Puntos seguidos se gana una Vida Extra
            lives += 1;
            lifeAdded = true;
        }
    }

    // Se ejecuta varias veces por Frame, en cada evento de GUI
    private void OnGUI() {
        for(int i=0; i<imgLives.Length; i++) {
            imgLives[i].SetActive(i<(lives-1));  // Activar y Desactivar Imágenes de Vidas
        }
        txtScore.text = string.Format("{0,4:D4}", score);  // Formatear texto de Puntuación
        txtMaxScore.text = string.Format("{0,4:D4}", maxScore);  // Formatear texto de Puntuación Máxima

        if(lives <= 0) {
            txtMessage.gameObject.SetActive(true);  // Activar mensaje de Game Over
        }
    }

    // Perder una Vida
    public void LoseLife() {
        if(lives > 0) {
            lives--;
        }
    }

    // Actualizar Puntuaciones del Juego
    public void UpdateScore(int points) {
        score += points;  // Sumar Puntos al Marcador
        if (score < 0) {
            score = 0;
        }
        if (score > maxScore) {
            maxScore = score;  // Si la Puntuación es mayor a la Puntuación Máxima se actualiza el valor.
        }
    }

    // Resetear Puntuación
    public void ResetScore() {
        score = 0;
        lifeAdded = false;
    }
}
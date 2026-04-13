using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar escenas

public class GameMenu : MonoBehaviour
{
    // Esta es la que ya tenías para entrar
    public void LoadWorldMap()
    {
        SceneManager.LoadScene(1); // O el nombre de tu escena de mapa
    }

    // AÑADE ESTA NUEVA FUNCIÓN para volver
    public void BackToMainMenu()
    {
        // Esto cargará la escena que está en la posición 0 (tu Inicio)
        SceneManager.LoadScene(0);
    }
}
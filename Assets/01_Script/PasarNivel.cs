using UnityEngine; 
using UnityEngine.SceneManagement;

public class PasarNivel : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Esto saldrá en la consola SIEMPRE que algo toque la puerta
        Debug.Log("Algo tocó la puerta: " + collision.gameObject.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("¡Es el Player! Intentando cambiar de nivel...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

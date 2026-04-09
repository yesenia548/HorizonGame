using UnityEngine;

public class IslandSelector : MonoBehaviour
{
    private SpriteRenderer rend;
    public Color hoverColor = Color.cyan; // Color al pasar el mouse
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        originalColor = rend.color;
    }

    void OnMouseEnter()
    {
        rend.color = hoverColor;
        Debug.Log("El mouse toco la isla");
    }

    void OnMouseExit()
    {
        rend.color = originalColor;
    }

    void OnMouseDown()
    {
        Debug.Log("Click en: " + gameObject.name);
        // Aquí es donde luego pondremos que cargue un nivel
    }
}
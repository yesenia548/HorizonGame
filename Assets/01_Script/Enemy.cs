using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Rigidbody2D rb2D;
    public float velocidadMovimiento;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.linearVelocity = new Vector2(velocidadMovimiento, rb2D.linearVelocity. y);
        MirarEnDireccionDelMovimiento();
        
    }


    public void MirarEnDireccionDelMovimiento()
    {
        if ((velocidadMovimiento > 0 && !MirandoALaDerecha()) || (velocidadMovimiento < 0 && MirandoALaDerecha()))
        {
            Girar();
        }
    }
    public void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    public bool MirandoALaDerecha()
    {
        return transform.localScale.x == -1;
    }
}

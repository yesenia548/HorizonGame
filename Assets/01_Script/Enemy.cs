using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Rigidbody2D rb2D;
    public float velocidadMovimientoBase;
    public float velocidadMovimientoActual;
    public Transform controladorFrente;
    public float distanciaRayoFrente;
    public LayerMask capasSuelo;
    public bool tocandoSueloFrente;



    public void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
        rb2D.linearVelocity = new Vector2(velocidadMovimientoActual, rb2D.linearVelocity. y);
        MirarEnDireccionDelMovimiento();

        tocandoSueloFrente = Physics2D.Raycast(controladorFrente.position, transform.right * -1, distanciaRayoFrente, capasSuelo);

    }


    public void MirarEnDireccionDelMovimiento()
    {
        if ((velocidadMovimientoActual > 0 && !MirandoALaDerecha()) || (velocidadMovimientoActual < 0 && MirandoALaDerecha()))
        {
            Girar();
        }
    }
    public void Girar()
    {
        Vector3 rotacion = transform.eulerAngles;
        rotacion.y = rotacion.y == 0 ? 180 : 0;
        transform.eulerAngles = rotacion;
    }

    public bool MirandoALaDerecha()
    {
        return transform.eulerAngles.y == 100;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorFrente.position, controladorFrente.position + distanciaRayoFrente * transform.right * -1);
    }
}

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 0.2f;

    public int health = 3;
    private Animator animator; // Para activar la animación de muerte
    private SpriteRenderer spriteRenderer;


    private Rigidbody2D rb;
    private Vector2 movement;



    public void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtenemos el Animator
        spriteRenderer = GetComponent<SpriteRenderer>(); // Para el efecto visual
    }

    // Update is called once per frame
    public void Update()
    {
        float distanceToPlay =Vector2.Distance(transform.position, player.position);
        if (distanceToPlay < detectionRadius)
        {
            Vector2 direccion =(player.position - transform.position).normalized;
            movement = new Vector2(direccion.x, 0);

            // Giro automático: Que el enemigo mire al jugador
            if (direccion.x > 0) transform.localScale = new Vector3(1, 1, 1);
            else if (direccion.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            movement = Vector2.zero;
        }

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Vida del enemigo: " + health);

        // Feedback visual: Brilla en rojo al ser golpeado
        spriteRenderer.color = Color.red;
        Invoke("ResetColor", 0.1f);

        if (health <= 0)
        {
            Die();
        }
    }

    void ResetColor()
    {
        spriteRenderer.color = Color.white;
    }

    void Die()
    {
        Debug.Log("El enemigo ha sido derrotado");

        // Si tienes el Trigger "Death" en el Animator (el que vimos antes)
        if (animator != null) animator.SetTrigger("Dead");

        // Detener el movimiento para que no siga persiguiendo muerto
        speed = 0;
        rb.simulated = false; // Desactiva la física

        // Lo destruye después de 1 segundo para que se vea la animación
        Destroy(gameObject, 1.0f);


    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}

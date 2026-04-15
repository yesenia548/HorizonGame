using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed = 2;
    private Rigidbody2D rb2D;

    private float move;
    public float jumpForce = 4;
    private bool isGrounded;
    public Transform groundCheck;
    public float grounRadius = 0.1f;
    public LayerMask groundLayer;

    private Animator animator;

    private int coins;
    public TMP_Text textCoins;

    public AudioSource audioSourse;

    public AudioClip coinClip;
    public AudioClip barreClip;

    public Transform attackPoint; // Un objeto vacío frente al jugador
    public float attackRange = 0.5f; // Qué tan lejos llega el golpe
    public LayerMask enemyLayer; // Para que solo golpee a los enemigos
    public AudioClip attackClip; // Sonido de espada/golpe

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        rb2D.linearVelocity = new Vector2(move * speed, rb2D.linearVelocity.y);

        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpForce);
        }

        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetFloat("VerticalVelocity", rb2D.linearVelocity.y);
        animator.SetBool("IsGrounded", isGrounded);

        if (Input.GetKeyDown(KeyCode.Z)) // O Input.GetButtonDown("Fire1")
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, grounRadius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Coin"))
        {
            audioSourse.PlayOneShot(coinClip);
            Destroy(collision.gameObject);
            coins++;
            textCoins.text=coins.ToString();
        }
            
        if (collision.transform.CompareTag("Spikes"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.transform.CompareTag("Barrel"))
        {
            audioSourse.PlayOneShot(barreClip);
            Vector2 knockbackDir = (rb2D.position - (Vector2)collision.transform.position).normalized;
            rb2D.linearVelocity = Vector2.zero;
            rb2D.AddForce(knockbackDir * 3, ForceMode2D.Impulse);

            BoxCollider2D[] colliders = collision.gameObject.GetComponents<BoxCollider2D>();

            foreach (BoxCollider2D col in colliders)
            {
                col.enabled =  false;   
            }

            collision.GetComponent<Animator>().enabled = true;
            Destroy(collision.gameObject, 0.5f);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        if (attackClip != null) audioSourse.PlayOneShot(attackClip);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D hit in hitEnemies)
        {
            Enemy enemyScript = hit.GetComponent<Enemy>();

            if (enemyScript != null)
            {
                enemyScript.TakeDamage(1);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

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
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, grounRadius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Coin"))
        {
            //audioSourse.PlayOneShot(coinClip);
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
}

using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Variables de Movimiento
    public float moveSpeed = 5f;    
    public float jumpForce = 10f;
    public float glideFallSpeed = -2f; //Velocidad de caída de planeo

    //Interacción con el suelo
    public Transform groundCheck;         
    public LayerMask groundLayer;         
    public float groundCheckRadius = 0.2f;

    // Variables privadas
    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private bool isFacingRight = true;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //Input horizontal

        if (Input.GetButtonDown("Jump") && isGrounded) //Input vertical
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButton("Jump") && !isGrounded && rb.linearVelocity.y < 0) //Input para planear
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, glideFallSpeed); //Reducción de la velocidad de caída, hace el efecto de planeo
        }

        if (horizontalInput != 0) //correr - idle
            animator.SetBool("IsRunning", true); //Se vale no escribir las llaves si es solo una línea, en caso contrario ps pon llaves lmao
        else
            animator.SetBool("IsRunning", false);

        // RESET
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsGliding", false);

        // SALTO NORMAL
        if (!isGrounded)
        {
            animator.SetBool("IsJumping", true);
        }

        // PLANEO
        if (!isGrounded &&
            rb.linearVelocity.y < 0 &&
            Input.GetButton("Jump"))
        {
            animator.SetBool("IsGliding", true);
        }


        FlipSprite();
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y); //Velocidad horizontal y de salto

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); //Veifica si el personaje está en el suelo
    }

    void FlipSprite()
    {
        // Si nos movemos a la izquierda (input < 0) pero miramos a la derecha (isFacingRight = true)
        if (horizontalInput < 0 && isFacingRight)
        {
            // Volteamos al jugador
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = false; // Ahora miramos a la izquierda
        }
        // Si nos movemos a la derecha (input > 0) pero miramos a la izquierda (isFacingRight = false)
        else if (horizontalInput > 0 && !isFacingRight)
        {
            // Volteamos al jugador
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = true; // Ahora miramos a la derecha
        }
    }
}

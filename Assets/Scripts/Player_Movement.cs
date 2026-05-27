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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y); //Velocidad horizontal y de salto

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); //Veifica si el personaje está en el suelo
    }

}

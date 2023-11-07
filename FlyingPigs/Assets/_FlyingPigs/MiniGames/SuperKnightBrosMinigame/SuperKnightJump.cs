using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperKnightJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float downForce = 5.0f;
    private Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    public LayerMask groundLayer;
    [SerializeField] bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        // Controlla se è stato toccato lo schermo (adatto per dispositivi mobili)
        if (Input.GetMouseButtonDown(0))
        {
            isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);
            if (isGrounded)
            {
                Jump();

            }
        } 
    }

    private void FixedUpdate()
    {
        if (transform.position.y >= 0)
        {
            // Applica una forza verso il basso quando sopra o a un'altitudine specifica
            rb.AddForce(Vector2.down *downForce, ForceMode2D.Impulse);
            Debug.Log("vai giu");
        }
    }

    private void Jump()
    {
        // Applica una forza verso l'alto per effettuare il salto
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

}


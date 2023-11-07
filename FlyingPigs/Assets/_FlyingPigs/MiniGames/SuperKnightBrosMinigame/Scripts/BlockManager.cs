using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class BlockManager : MonoBehaviour
{
    public float moveSpeed = 3.5f; // Velocità di movimento
    private bool movingRight = true; // Direzione iniziale
    [SerializeField] Animator animator;

    // Variabili per rilevare i contatti

    private void Update()
    {
        // Movimento laterale
        MoveHorizontally();

        // Rileva i contatti
        CheckContacts();
    }

    void MoveHorizontally()
    {
        float horizontalInput = movingRight ? 1.0f : -1.0f;
        Vector3 movement = new Vector3(horizontalInput, 0, 0);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
        //movespeed deve essere moltiplicata * il livello del minigioco

        // Cambia direzione quando raggiunge il limite destro o sinistro
        if (transform.position.x >= 2f)
        {
            movingRight = false;
        }
        else if (transform.position.x <= -2f)
        {
            movingRight = true;
        }
    }

    void CheckContacts()
{
    
    // Raycast a sinistra
    RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.6f);
    RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.6f);

    // Raycast verso l'alto
    RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.6f);

    // Visualizza i raycast
    Debug.DrawRay(transform.position, Vector2.left * 0.6f, Color.green); // Raycast a sinistra (verde)
    Debug.DrawRay(transform.position, Vector2.right * 0.6f, Color.blue); // Raycast a destra (blu)
    Debug.DrawRay(transform.position, Vector2.up * 0.6f, Color.yellow); // Raycast verso l'alto (giallo)

    // Esempio di come controllare i risultati dei raycast

    if (hitLeft.collider != null && hitLeft.collider.CompareTag("Player") || hitRight.collider != null&& hitRight.collider.CompareTag("Player"))
    {
        // Il raycast laterale ha colpito qualcosa
        Debug.Log("Toccato di lato");
    }

    if (hitUp.collider != null&& hitUp.collider.CompareTag("Player"))
    {
        // Il raycast verso l'alto ha colpito qualcosa
        Debug.Log("Toccato dall'alto: " + hitUp.collider.name);
    }
}

private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Codice da eseguire quando il collider entra in collisione con un oggetto contrassegnato come "Player".
            Debug.Log("Collisione con il giocatore!");
            animator.SetBool("isBroken", true);
            
        }
    }
    public void BlockBreaker()
    {
        Destroy(this.gameObject);
    }
}



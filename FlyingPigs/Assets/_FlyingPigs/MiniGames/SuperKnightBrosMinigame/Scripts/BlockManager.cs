using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class BlockManager : MonoBehaviour
{
    public float moveSpeed = 3.5f; // Velocità di movimento
    private bool movingRight = true; // Direzione iniziale
    private bool isDead = false;
    [SerializeField] private SuperKnightJump skj;
    [SerializeField] Animator animator;
    [SerializeField] private AudioManager audioManager;

    // Variabili per rilevare i contatti

    private void Update()
    {
        if(!isDead) {
            // Movimento laterale
            MoveHorizontally();
        }
    }

    void MoveHorizontally()
    {
        float horizontalInput = movingRight ? 1.0f : -1.0f;
        Vector3 movement = new Vector3(horizontalInput, 0, 0);
        transform.Translate(movement * SuperKnightJump.level * moveSpeed * Time.deltaTime);
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

private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioManager.PlaySound(audioManager.rockSmash);
            if (!isDead) {
                // Codice da eseguire quando il collider entra in collisione con un oggetto contrassegnato come "Player".
                isDead = true;
                //Debug.Log("Collisione con il giocatore!");
                animator.SetBool("isBroken", true);
                skj.OnBlockDestroyed();
            }
            
        }
    }
public void BlockBreaker()
{

    Destroy(this.gameObject);
}
}



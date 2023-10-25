using System;
using UnityEngine;

public class PigScript : MonoBehaviour
{
    public static event Action OnDeath;
    public static event Action OnScore;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float force;
    [SerializeField] private float yBound;

    private void Start(){
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && rigidBody.position.y < yBound){
            Flap();
        }
    }

    private void OnCollisionEnter2D(){
        OnDeath?.Invoke();

        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(){
        OnScore?.Invoke();
    }

    private void Flap(){
        rigidBody.velocity = Vector2.zero;
        rigidBody.AddForce(Vector2.up * force);
    }
}

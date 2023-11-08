using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuperKnightJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float downForce = 5.0f;
    private Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    public LayerMask groundLayer;
    [SerializeField] bool isGrounded;
    [SerializeField] Animator animator;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] public ImageShow imageShow;
    [SerializeField] public Timer timer;
    [SerializeField] private Coordinator coordinator;
    public static float timeElapsed = 0f;
    public static int level = 1;
    private bool canMove = true;
    private bool gameOver = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.touchCount > 0 && canMove) {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began){
                isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);
                if (isGrounded) {
                    Jump();
                    animator.SetBool("isJumping",true);

                }
            }
        }

        if(!timer.GetCounting() && !gameOver) {
            gameOver = true;
            canMove = false;
            audioManager.PlaySound(audioManager.endMinigameFail);
            timeElapsed += 10 - timer.GetTime();
            timer.PauseTimer();
            StartCoroutine(PlayerDeath(2f));
        }

    }
    private void FixedUpdate()
    {
        if (transform.position.y >= 0)
        {
            // Applica una forza verso il basso quando sopra o a un'altitudine specifica
            rb.AddForce(Vector2.down *downForce, ForceMode2D.Impulse);
            //Debug.Log("vai giu");
        }
        if (animator.GetBool("isJumping")==true)
        {
            isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);
            if (isGrounded)
            {
                animator.SetBool("isJumping",false);

            }
        }
    }

    private void Jump()
    {
        // Applica una forza verso l'alto per effettuare il salto
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void OnBlockDestroyed() {
        canMove = false;
        imageShow.SwitchShow(true);
        timeElapsed += 10 - timer.GetTime();
        timer.PauseTimer();
        if (level >= 5) {
            audioManager.PlaySound(audioManager.endMinigameSucc);
        }
        StartCoroutine(EndLevelAfterTime(2f));
    }
    private void OnEndLevel() {
        level++;
        if(level <= 5) {
            RestartGame();
        }
        else {
            OnGameOver();
        }
    }

    public IEnumerator PlayerDeath(float time){
        yield return new WaitForSecondsRealtime(time);

        OnGameOver();
    }

    private IEnumerator EndLevelAfterTime(float time){
        yield return new WaitForSecondsRealtime(time);

        OnEndLevel();
    }
    private void OnGameOver() {
        SerializableDictionary<GameStatsEnum,float> value = new SerializableDictionary<GameStatsEnum, float>(){
            {GameStatsEnum.TimeElapsed, timeElapsed},
            {GameStatsEnum.GameHealth, (level - 1) * 0.02f},
            {GameStatsEnum.GameCurrency, (level - 1) * 3}
        };
        GameStatisticsManager.Instance.updateStatsWith(value);

        level = 1;
        timeElapsed = 0f;

        coordinator.LoadScene("MainGameScene");

    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}


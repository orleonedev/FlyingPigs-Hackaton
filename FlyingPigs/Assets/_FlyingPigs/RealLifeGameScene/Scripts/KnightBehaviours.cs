using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KnightBehaviour : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject gameView;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Animator knightAnimator;
    [SerializeField] private TMP_Text expLabel;
    [SerializeField] private Image expBarFill;
    [SerializeField] private float expToLevelUp; //experience to level up
    [SerializeField] private float expLimitProgress; // experience max progress
    [SerializeField] protected float time;
    protected float elapsedTime;
    protected int knightLevel;
    protected int enemyLives;
    protected Animator enemyAnimator;

    private void Start(){
        knightLevel = 1;
        expLabel.text = "Lvl " + knightLevel.ToString();
        enemyAnimator = enemy.GetComponent<Animator>();
        SpawnEnemy();
    }

    private void Update()
    {
        if(enemyLives <= 0){
            elapsedTime += Time.deltaTime;

            if(elapsedTime > time && enemyLives <= 0){
                SpawnEnemy();
                elapsedTime = 0.0f;
            }
        }        

        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began){
                CheckTouch(touch.position);
            }
        }
    }

    private void CheckTouch(Vector3 pos){
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D tapped = Physics2D.OverlapPoint(touchPos);
     
        if(tapped && tapped == gameView.GetComponent<Collider2D>())
        {
            knightAnimator.SetBool("isAttacking", true); 
            //audioManager.PlaySound(audioManager.);
            
            if(enemy.activeInHierarchy){
                if(enemyLives > 0) {
                    enemyAnimator.SetBool("isAttacked", true);
                    enemyLives--;
                } else {
                    DestroyEnemy();
                }
            }
        }
    }

    public void StopAttack(){
        knightAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetBool("isAttacked", false);
    }

    public void SpawnEnemy(){
        enemyLives = UnityEngine.Random.Range(3, 10);
        enemy.SetActive(true);
    }

    public void DestroyEnemy(){
        enemy.SetActive(false);
        //qui recuperiamo i punti esperienza dati dal mostro. Per adesso diamo 1exp per ogni enemy ucciso
        var fillAmount = GetFillAmount(1.0f);

        if(expBarFill.fillAmount + fillAmount >= 1){
            expBarFill.fillAmount = (expBarFill.fillAmount + fillAmount)%1;
            knightLevel++;
            expLabel.text = "Lvl " + knightLevel.ToString();
            expToLevelUp *= expLimitProgress;
        } else {
            expBarFill.fillAmount += fillAmount;
        }
    }

    private float GetFillAmount(float expFromEnemy){
        return ((1*expFromEnemy)/expToLevelUp);
    }
}

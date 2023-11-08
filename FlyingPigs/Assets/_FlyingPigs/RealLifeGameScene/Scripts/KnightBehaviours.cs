using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KnightBehaviour : MonoBehaviour
{
    [SerializeField] private Animator fadingCanvaAnimator;
    [SerializeField] private CanvasGroup fadingCanvaGroup;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject gameView;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Animator knightAnimator;
    [SerializeField] private TMP_Text expLabel;
    [SerializeField] private Image expBarFill;
    [SerializeField] protected float time;
    protected float elapsedTime;
    protected int enemyLives;
    protected Animator enemyAnimator;

    private void Start(){
        expBarFill.fillAmount = FakeGameManager.Instance.expBarFill;
        expLabel.text = "Lvl " + FakeGameManager.Instance.knightLevel.ToString();
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
        Debug.Log("ALPHA: " + fadingCanvaGroup.alpha);
        if(tapped && tapped == gameView.GetComponent<Collider2D>() && (!fadingCanvaAnimator.GetBool("isDayOver") && fadingCanvaGroup.alpha == 0f))
        {
            knightAnimator.SetBool("isAttacking", true);
            
            if(enemy.activeInHierarchy){
                if(enemyLives > 0) {
                    enemyAnimator.SetBool("isAttacked", true);
                    enemyLives--;
                } else {
                    DestroyEnemy();
                    audioManager.PlaySound(audioManager.slimeDeath);
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
            FakeGameManager.Instance.knightLevel++;
            expLabel.text = "Lvl " + FakeGameManager.Instance.knightLevel.ToString();
            FakeGameManager.Instance.expToLevelUp *= FakeGameManager.Instance.expLimitProgress;
            audioManager.PlaySound(audioManager.levelUp);
        } else {
            expBarFill.fillAmount += fillAmount;
        }

        FakeGameManager.Instance.expBarFill = expBarFill.fillAmount;
    }

    private float GetFillAmount(float expFromEnemy){
        return ((1*expFromEnemy)/FakeGameManager.Instance.expToLevelUp);
    }

    public void PlayAttackSound(){
        audioManager.PlaySound(audioManager.attack);
    }
}

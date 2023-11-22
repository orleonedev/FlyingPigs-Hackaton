using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Animator knightAnimator;
    [SerializeField] private TMP_Text expLabel;
    [SerializeField] private Image expBarFill;
    [SerializeField] protected float time;
    protected float elapsedTime;
    protected int enemyLives;
    protected bool isEnemyAlive;
    protected Animator enemyAnimator;
    protected float spawnRate;
    protected bool doLerp = false;
    protected bool hasLeveledUp = false;
    protected float finalFillAmount;
    protected float animationTimer = 0.0f;
    private SerializableDictionary<GameStatsEnum,float> fixedUpdates = new SerializableDictionary<GameStatsEnum, float>(){
        {GameStatsEnum.GameHealth, 0.05f},
        {GameStatsEnum.GameCurrency, 2f}
    };

    private void Start(){
        expBarFill.fillAmount = GetFillAmount(FakeGameManager.Instance.expPoints);
        expLabel.text = "Lvl " + FakeGameManager.Instance.knightLevel.ToString();
        if(!isEnemyAlive){
            SpawnEnemy();
        }
    }

    private void Update()
    {
        if(enemyLives <= 0){
            elapsedTime += (Time.deltaTime*2);

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

        if(doLerp){
           // Increment the animation timer by the time that has passed since the last frame.
            animationTimer += Time.deltaTime * 1.5f;

            // Use Lerp to smoothly interpolate the game object's position between the start and end positions.
            if(!hasLeveledUp){
                expBarFill.fillAmount = Mathf.Lerp(expBarFill.fillAmount, finalFillAmount, animationTimer); 
            } else {
                expBarFill.fillAmount = Mathf.Lerp(0.0f, finalFillAmount, animationTimer); 
            }
            
            if (animationTimer >= 1.0f)
            {
                finalFillAmount = 0.0f;
                animationTimer = 0.0f;
                doLerp = false;
                hasLeveledUp = false;
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
        }
    }

    public void StopAttack(){
        knightAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetBool("isAttacked", false);
    }

    public void SpawnEnemy(){
        var spawnRate = UnityEngine.Random.Range(0f, 100f);

        switch(spawnRate){
            case <= 5:
                enemy = enemies[3];
                break;
            case <= 20:
                enemy = enemies[2];
                break;
            case <= 50:
                enemy = enemies[1];
                break;
            case <= 100:
                enemy = enemies[0];
                break;
            default:
                enemy = enemies[0];
                break;
        }

        enemyAnimator = enemy.GetComponent<Animator>();
        enemyLives = enemy.GetComponent<MainGameEnemy>().getLives();
        enemy.SetActive(true);
        isEnemyAlive = true;
    }

    public void DestroyEnemy(){
        enemy.SetActive(false);
        isEnemyAlive = false;

        var exp = enemy.GetComponent<MainGameEnemy>().getExpGiven();
        GameStatisticsManager.Instance.updateStatsWith(new SerializableDictionary<GameStatsEnum, float>(){
            {GameStatsEnum.GameCurrency, enemy.GetComponent<MainGameEnemy>().getGems()}
            });

        hasLeveledUp = false;

        while(exp > 0){
            if(FakeGameManager.Instance.expPoints + exp >= FakeGameManager.Instance.expToLevelUp){
                FakeGameManager.Instance.knightLevel++;
                GameStatisticsManager.Instance.updateStatsWith(fixedUpdates);
                expLabel.text = "Lvl " + FakeGameManager.Instance.knightLevel.ToString();
                var diffExp = FakeGameManager.Instance.expToLevelUp - FakeGameManager.Instance.expPoints;
                exp -= diffExp;
                FakeGameManager.Instance.expPoints = 0.0f;
                FakeGameManager.Instance.expToLevelUp = FakeGameManager.Instance.expLimitProgress * FakeGameManager.Instance.knightLevel; 
                hasLeveledUp = true;
            } else {
                FakeGameManager.Instance.expPoints += exp;
                exp = 0;
            }
        }

        if(hasLeveledUp){
            audioManager.PlaySound(audioManager.levelUp);
        }
        finalFillAmount = GetFillAmount(FakeGameManager.Instance.expPoints);
        doLerp = true;
    }

    private float GetFillAmount(float exp){
        return exp/FakeGameManager.Instance.expToLevelUp;
    }

    public void PlayAttackSound(){
        audioManager.PlaySound(audioManager.attack, 0.25f);
    }

    public void CheckEnemyAttack(){
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

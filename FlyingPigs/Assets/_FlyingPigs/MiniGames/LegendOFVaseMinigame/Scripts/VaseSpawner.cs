using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VaseSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blueVasePrefab;
    [SerializeField] private GameObject redVasePrefab;
    [SerializeField] private Transform playArea;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] public ImageShow imageShow;
    [SerializeField] public Timer timer;
    [SerializeField] private Coordinator coordinator;
    public static float timeElapsed = 0f;
    private static int level = 1;
    private bool gameOver = false;
    [SerializeField] private int totalNumberOfVases;   
    [SerializeField] private float blueVaseProbability; // Valore compreso tra 0 e 1, ad es. 0.7 per il 70% di probabilità di un vaso blu.
    [SerializeField] public int numberOfBlueVases = 0;
   
    // Lista per tenere traccia delle posizioni degli oggetti generati
    private List<Vector3> spawnedPositions = new List<Vector3>();
    private List<ClickableVase> vases = new List<ClickableVase>();

    void Start()
    {

        switch(Camera.main.aspect){
            case (float) 10/16:
            case (float) 9/16:
            case (float) 3/4:
                playArea.transform.localScale = new Vector3(6f, 8f, 0); //è una pezza
                break;
            default:
                playArea.transform.localScale = new Vector3(3.5f, 7.8f, 0);
                break;
        }
        
        totalNumberOfVases += 2 * (level - 1);
        
        for (int i = 0; i < totalNumberOfVases; i++) {

            // Genera una posizione casuale
            Vector3 spawnPosition = GetRandomSpawnPosition(playArea.position, playArea.localScale / 2.5f);
            // Verifica che la posizione soddisfi i criteri (distanza minima)
            while (!IsPositionValid(spawnPosition))
            {
                spawnPosition = GetRandomSpawnPosition(playArea.position, playArea.localScale / 2.5f);
            }

            // Aggiungi la posizione alla lista delle posizioni generata
            spawnedPositions.Add(spawnPosition);

            // Scegli casualmente tra il prefab "BlueVase" e "RedVase" tenendo conto della blue vase probability
            GameObject selectedPrefab = Random.Range(0f, 1f) < blueVaseProbability ? blueVasePrefab : redVasePrefab;
            if (selectedPrefab == blueVasePrefab)
            {
                numberOfBlueVases++;
            }

            // Istanzia il prefab scelto alla posizione generata
            GameObject vase = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

            // Aggiungi alla lista dei vasi
            vases.Add(vase.GetComponent<ClickableVase>());

            // Imposta il "playArea" come genitore dell'oggetto
            vase.transform.parent = playArea;
        }
    }

    void Update() {
        if(!timer.GetCounting() && !gameOver) {
            gameOver = true;
            audioManager.PlaySound(audioManager.endMinigameFail, 1f);
            timeElapsed += 10 - timer.GetTime();
            foreach(ClickableVase vase in vases) {
                vase.canBeTouched = false;
            }
            StartCoroutine(PlayerDeath(2f));
        }

        if(numberOfBlueVases == 0 && !gameOver) {
            gameOver = true;
            foreach(ClickableVase vase in vases) {
                vase.canBeTouched = false;
            }
            imageShow.SwitchShow(true);
            timeElapsed += 10 - timer.GetTime();
            timer.PauseTimer();
            if (level >= 5) {
                audioManager.PlaySound(audioManager.endMinigameSucc, 1f);
            }
            StartCoroutine(EndLevelAfterTime(2f));
        } 
    }

    // Restituisce una posizione casuale nell'intervallo desiderato
    Vector3 GetRandomSpawnPosition(Vector3 center, Vector3 size)    {
        float x = Random.Range(center.x - size.x, center.x + size.x);        
        float y = Random.Range(center.y - size.y, center.y + size.y);        
        return new Vector3(x, y, 0f);
    }

    // Verifica se una posizione è valida (distanza minima da altre posizioni)
    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 otherPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, otherPosition) < 0.8f)
            {
                return false;
            }
        }
        return true;
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

    public void RedVaseDestroyed() {
        audioManager.PlaySound(audioManager.endMinigameFail, 1f);
        imageShow.SwitchShow(false);
        timeElapsed += 10 - timer.GetTime();
        timer.PauseTimer();
        foreach(ClickableVase vase in vases) {
            vase.canBeTouched = false;
        }
        StartCoroutine(PlayerDeath(2f));
    }
    
}


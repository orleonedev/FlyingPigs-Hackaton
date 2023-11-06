using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    private GameStats gameStats;
    [SerializeField] private Coordinator coordinator;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private List<GameObject> GoodEndings;
    [SerializeField] private List<GameObject> BadEndings;
    public bool isBadEnding;
    private int numOfAudioSource = -1;

    private void Awake(){
        gameStats = GameStats.Instance;
    }

    private void Start(){
        isBadEnding = GameStatisticsManager.Instance.CheckDepleatedStats();
        ChooseEnding();
        PlayEndTheme();
    }

    private void PlayEndTheme(){
        if(isBadEnding){ 
            numOfAudioSource = audioManager.PlaySoundLoop(audioManager.BadEndingClip);
        } else {
            numOfAudioSource = audioManager.PlaySoundLoop(audioManager.GoodEndingClip);
        }
    }

    public void ChooseEnding(){
        if(GameStatisticsManager.Instance.CheckDepleatedStats()){
            if(gameStats.RealHealth <= 0f){
                BadEndings.First(obj => obj.name == "Divorzio").SetActive(true);
            } else if(gameStats.GameHealth <= 0f){
                BadEndings.First(obj => obj.name == "Cacciato dalla Gilda").SetActive(true);
            } else if(gameStats.RealMoney <= 0f){
                BadEndings.First(obj => obj.name == "Bancarotta").SetActive(true);
            }
        } else {
            if(gameStats.GameHealth >= 0.6f && gameStats.RealHealth >= 0.6f && gameStats.RealMoney >= 600f){
                GoodEndings.First(obj => obj.name == "Vincente al gioco della vita").SetActive(true);
            } else if(gameStats.GameHealth > gameStats.RealHealth && gameStats.GameHealth >= 0.5f){
                GoodEndings.First(obj => obj.name == "Capogilda").SetActive(true);
            } else if(gameStats.RealHealth > gameStats.GameHealth && gameStats.RealHealth >= 0.5f){
                GoodEndings.First(obj => obj.name == "Marito Perfetto").SetActive(true);
            } else {
                if(gameStats.RealMoney > 600f){
                    GoodEndings.First(obj => obj.name == "Uomo Equilibrato").SetActive(true);
                } else {
                    GoodEndings.First(obj => obj.name == "Uomo Distrutto").SetActive(true);
                }
            }
        }
    }

    public void EndGame(){
        if (numOfAudioSource != -1) {
            audioManager.StopSoundLoop(numOfAudioSource);
            numOfAudioSource = -1;
        }
        coordinator.LoadScene("MainScene");
    }
}

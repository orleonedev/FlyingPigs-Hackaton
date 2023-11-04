using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{

    [SerializeField]
    private Coordinator coordinator;
    private MinigamesList minigamesListObj;

    public List<GameObject> loadingScreens;
    private int rng;
    private string selectedMinigame;

    void Awake() {
        minigamesListObj = MinigamesList.Instance;
    }
    void Start()
    {
        selectedMinigame = selectRandomGame();
        loadingScreens.First(obj => obj.name == selectedMinigame).SetActive(true);
        //loadingScreens[rng].SetActive(true);
    }

    void Update()
    {
        // if(Input.GetMouseButtonDown(0)){
        //     foreach(GameObject loadingScreen in loadingScreens){
        //         if(loadingScreen.activeInHierarchy){
        //             loadingScreen.SetActive(false);
        //         }
        //     }
            
        // }

        if(Input.GetMouseButtonDown(0)) {
            onStartNewMinigame();
        }
    }

    public string selectRandomGame() {
        List<string> filtered = minigamesListObj.MinigamesNames.Except(minigamesListObj.PlayedGamesOfTheDay).ToList();
        var rand = new System.Random();
        int randomIndex = rand.Next(filtered.Count);
        string minigameName = filtered.ElementAt(randomIndex);
        minigamesListObj.PlayedGamesOfTheDay.Add(minigameName);
        return minigameName;
    }
    
    public void onStartNewMinigame() {
        coordinator.LoadScene(selectedMinigame);
    }
}

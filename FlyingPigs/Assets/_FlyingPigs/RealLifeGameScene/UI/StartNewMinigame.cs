using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StartNewMinigame : MonoBehaviour
{
    [SerializeField]
    private Coordinator coordinator;

    private MinigamesList minigamesListObj; 

    void Awake() {
        minigamesListObj = MinigamesList.Instance;
    }
    // Start is called before the first frame update
    public void onStartNewMinigame() {
        List<string> filtered = minigamesListObj.MinigamesNames.Except(minigamesListObj.PlayedGamesOfTheDay).ToList();
        var rand = new System.Random();
        int randomIndex = rand.Next(filtered.Count);
        string minigameName = filtered.ElementAt(randomIndex);
        minigamesListObj.PlayedGamesOfTheDay.Add(minigameName);

        coordinator.LoadScene(minigameName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCanvasManager : MonoBehaviour
{
    public GameObject activeGameView;
    public GameObject mainGameView;
    public GameObject caveGameView;
    public KnightBehaviour knight;

    public void ChangeGameView(){
        if(GameStats.Instance.Day % 2 == 0){
            activeGameView = caveGameView;
            caveGameView.SetActive(true);
            mainGameView.SetActive(false);
        } else {
            activeGameView = mainGameView;
            caveGameView.SetActive(false);
            mainGameView.SetActive(true);
        }
    }
}

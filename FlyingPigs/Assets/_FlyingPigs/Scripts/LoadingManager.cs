using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    public GameObject[] loadingScreens;
    private int rng;

    void Start()
    {
        rng = UnityEngine.Random.Range(0, loadingScreens.Length);
        loadingScreens[rng].SetActive(true);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            foreach(GameObject loadingScreen in loadingScreens){
                if(loadingScreen.activeInHierarchy){
                    loadingScreen.SetActive(false);
                }
            }
            rng = UnityEngine.Random.Range(0, loadingScreens.Length);
            loadingScreens[rng].SetActive(true);
        }
    }
}

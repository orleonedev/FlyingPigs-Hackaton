using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="FakeGameManager", menuName ="FakeGameManagerScriptableObj")]
public class FakeGameManager : SingletonScriptableObject<FakeGameManager>
{
    public float expPoints;
    //public float expBarFill;
    public float expToLevelUp; //experience to level up
    public float expLimitProgress; // experience max progress
    public int knightLevel;

    public void ReloadGame(){
        expPoints = 0.0f;
        //expBarFill = 0.0f;
        expToLevelUp = 5.0f;
        expLimitProgress = 5.0f;
        knightLevel = 1;
    }
}

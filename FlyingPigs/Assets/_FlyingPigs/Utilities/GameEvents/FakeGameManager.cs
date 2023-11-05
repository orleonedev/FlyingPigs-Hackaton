using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="FakeGameManager", menuName ="FakeGameManagerScriptableObj")]
public class FakeGameManager : SingletonScriptableObject<FakeGameManager>
{
    public float expBarFill;
    public float expToLevelUp; //experience to level up
    public float expLimitProgress; // experience max progress
    public int knightLevel;

    public void ReloadGame(){
        expBarFill = 0.0f;
        expToLevelUp = 10.0f;
        expLimitProgress = 1.5f;
        knightLevel = 1;
    }
}

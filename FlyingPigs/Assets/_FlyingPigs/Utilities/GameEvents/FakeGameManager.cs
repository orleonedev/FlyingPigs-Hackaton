using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="FakeGameManager", menuName ="FakeGameManagerScriptableObj")]
public class FakeGameManager : SingletonScriptableObject<FakeGameManager>
{
    public float expBarFill = 0.0f;
    public float expToLevelUp = 10.0f; //experience to level up
    public float expLimitProgress = 1.5f; // experience max progress
    public int knightLevel = 1;
}

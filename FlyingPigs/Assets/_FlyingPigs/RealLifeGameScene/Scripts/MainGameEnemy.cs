using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameEnemy : MonoBehaviour
{   
    [SerializeField] private int lives;
    [SerializeField] private float expGiven;
    [SerializeField] private float gems;

    public int getLives(){
        return lives;
    }

    public float getExpGiven(){
        return expGiven;
    }

    public float getGems(){
        return gems;
    }
}

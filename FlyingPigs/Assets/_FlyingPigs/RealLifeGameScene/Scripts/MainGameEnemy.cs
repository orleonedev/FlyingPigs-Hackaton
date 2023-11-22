using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameEnemy : MonoBehaviour
{   
    [SerializeField] private int lives;
    [SerializeField] private float expGiven;
    [SerializeField] private float gems;
    public int damageTaken = 0;

    public int GetLives(){
        return lives;
    }

    public float GetExpGiven(){
        return expGiven;
    }

    public float GetGems(){
        return gems;
    }

    public void TakeDamage(int damage){
        damageTaken += damage; 
    }

    public int GetDamageTaken(){
        return damageTaken;
    }

    public void ResetDamageTaken(){
        damageTaken = 0;
    }

    public bool IsAlive(){
        if(damageTaken < lives - 1){
            return true;
        } else {
            return false;
        }
    }
}
